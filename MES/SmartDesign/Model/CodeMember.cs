using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartCommon.Common;

namespace SmartDesign.Model
{
    class CodeMember
    {

        #region Properties
        public string CorpId { get; set; }
        public string MastID { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public DateTime CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime ModDt { get; set; }

        // Detail Properties  
        public string EmpNo { get; set; }
        public string WorkJob { get; set; }
        public string WorkType { get; set; }
        public string DetailRemark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        // 마스터 조회
        public DataTable SelectCodeMemberMaster()
        {
            query = $@"
                        SELECT 
                            CorpId,
                            MastID,
                            Title,
                            Remark
                        FROM CodeMemberMaster 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ORDER BY MastID
                    ";
            return db.GetDataTable(query);
        }

        // 디테일 조회
        public DataTable SelectCodeMemberDetail(string mastID)
        {
            query = $@"
                        SELECT 
                            cmd.CorpId,
                            cmd.MastID,
                            cmd.EmpNo,
                            cu.EmployeeName,
                            cmd.WorkJob,
                            ci1.Title AS RoleName,
							cmd.WorkType,                           
                            ci2.Title AS PermissionName,
                            cmd.Remark
                        FROM CodeMemberDetail cmd
                        LEFT JOIN CodeUser cu ON cmd.CorpId = cu.CorpId AND cmd.EmpNo = cu.EmployeeId
                        LEFT JOIN CmCateItem ci1 ON ci1.CorpId = cmd.CorpId AND ci1.Class = 'PJ70' AND ci1.ItemCode = cmd.WorkJob
                        LEFT JOIN CmCateItem ci2 ON ci2.CorpId = cmd.CorpId AND ci2.Class = 'PJ80' AND ci2.ItemCode = cmd.WorkType
                        WHERE cmd.CorpId = '{WrGlobal.CorpID}' 
                        AND cmd.MastID = '{mastID}'
                        ORDER BY cmd.WorkType
                    ";
            return db.GetDataTable(query);
        }

        // 마스터 저장 (INSERT/UPDATE)
        public bool InsUpd_CodeMemberMaster()
        {
            query = $@"
                        IF EXISTS (
                            SELECT MastID 
                            FROM CodeMemberMaster 
                            WHERE CorpId = '{CorpId}' AND MastID = '{MastID}'
                        )
                        BEGIN
                            UPDATE CodeMemberMaster
                            SET Title = '{Title}',
                                Remark = '{Remark}'
                            WHERE CorpId = '{WrGlobal.CorpID}' AND MastID = '{MastID}'
                        END
                        ELSE
                        BEGIN
                            INSERT INTO CodeMemberMaster (
                                CorpId, MastID, Title, Remark
                            )
                            VALUES (
                                '{WrGlobal.CorpID}', '{MastID}', '{Title}', '{Remark}'
                            )
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        // 디테일 저장 (INSERT/UPDATE)
        public bool InsUpd_CodeMemberDetail()
        {
            query = $@"
                        IF EXISTS (
                            SELECT EmpNo 
                            FROM CodeMemberDetail 
                            WHERE CorpId = '{WrGlobal.CorpID}' AND MastID = '{MastID}' AND EmpNo = '{EmpNo}'
                        )
                        BEGIN
                            UPDATE CodeMemberDetail
                            SET WorkJob = '{WorkJob}',
                                WorkType = '{WorkType}',
                                Remark = '{DetailRemark}'
                            WHERE CorpId = '{WrGlobal.CorpID}' AND MastID = '{MastID}' AND EmpNo = '{EmpNo}'
                        END
                        ELSE
                        BEGIN
                            INSERT INTO CodeMemberDetail (
                                CorpId, MastID, EmpNo, WorkJob, WorkType, Remark
                            )
                            VALUES (
                                '{WrGlobal.CorpID}', '{MastID}', '{EmpNo}', '{WorkJob}', '{WorkType}', '{DetailRemark}'
                            )
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        // 마스터 삭제
        public bool Del_CodeMemberMaster()
        {
            query = $@"
                        -- 디테일 먼저 삭제
                        DELETE FROM CodeMemberDetail 
                        WHERE CorpId = '{CorpId}' AND MastID = '{MastID}'

                        -- 마스터 삭제
                        DELETE FROM CodeMemberMaster 
                        WHERE CorpId = '{CorpId}' AND MastID = '{MastID}'
                    ";
            return db.ExecuteQuery(query);
        }

        // 디테일 삭제
        public bool Del_CodeMemberDetail()
        {
            query = $@"
                        DELETE FROM CodeMemberDetail 
                        WHERE CorpId = '{CorpId}' AND MastID = '{MastID}' AND EmpNo = '{EmpNo}'
                    ";
            return db.ExecuteQuery(query);
        }

        public bool DelAll_CodeMemberDetail()
        {
            query = $@"
                DELETE FROM CodeMemberDetail 
                WHERE CorpId = '{CorpId}' AND MastID = '{MastID}'
            ";
            return db.ExecuteQuery(query);
        }

        // 다음 MastID 생성
        public string GetNextMastID()
        {
            query = $@"
                        SELECT ISNULL(MAX(CAST(MastID AS INT)), 0) + 10 AS NextMastID
                        FROM CodeMemberMaster 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ISNUMERIC(MastID) = 1
                    ";

            DataRow dr = db.GetDataRow(query);
            return dr != null ? dr["NextMastID"].ToString() : "10";
        }

        // MastID 중복 체크
        public bool CheckMastIDExists(string mastID)
        {
            query = $@"
                        SELECT COUNT(*) AS CNT
                        FROM CodeMemberMaster 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND MastID = '{mastID}'
                    ";
            DataRow dr = db.GetDataRow(query);
            return dr != null && Convert.ToInt32(dr["CNT"]) > 0;
        }


        #region 프로젝트 멤버 테이블 (프로젝트 담당 멤버) 
        // 프로젝트 멤버 조회
        public DataTable SelectProjectMember(string projectID)
        {
            query = $@"
                        SELECT 
                              pm.CorpId,
                              pm.ProjectID,
                              pm.EmpNo,
                              cu.EmployeeName,
                              pm.WorkJob,
                              ci1.Title AS RoleName,
                              pm.WorkType,
                              ci2.Title AS PermissionName,
                              pm.Remark
                        FROM ProjectMember pm
                        LEFT JOIN CodeUser cu ON pm.CorpId = cu.CorpId AND pm.EmpNo = cu.EmployeeId
                        LEFT JOIN CmCateItem ci1 ON ci1.CorpId = pm.CorpId AND ci1.Class = 'PJ70' AND ci1.ItemCode = pm.WorkJob
                        LEFT JOIN CmCateItem ci2 ON ci2.CorpId = pm.CorpId AND ci2.Class = 'PJ80' AND ci2.ItemCode = pm.WorkType
                        WHERE pm.CorpId = '{WrGlobal.CorpID}' AND pm.ProjectID = '{projectID}'
                        ORDER BY pm.WorkType
                    ";
            return db.GetDataTable(query);
        }
        // CodeMemberDetail에서 ProjectMember로 복사
        public bool CopyToProjectMember(string projectID)
        {
            query = $@"
                        INSERT INTO ProjectMember(CorpId, ProjectID, EmpNo, WorkJob, WorkType, Remark)
                        SELECT CorpId, '{projectID}' AS ProjectID, EmpNo, WorkJob, WorkType, Remark
                        FROM CodeMemberDetail 
                        WHERE CorpId = '{CorpId}' 
                          AND MastID = '{MastID}'
            ";
            return db.ExecuteQuery(query);
        }

        // 기존 프로젝트 멤버 삭제 (필요시)
        public bool DelProjectMember(string projectID)
        {
            query = $@"
                        DELETE FROM ProjectMember 
                         WHERE CorpId = '{CorpId}' 
                           AND ProjectID = '{projectID}'
            ";
            return db.ExecuteQuery(query);
        }
        #endregion 
    }
}