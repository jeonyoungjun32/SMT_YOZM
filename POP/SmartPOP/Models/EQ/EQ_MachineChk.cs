
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Model.EQ
{
    class EQ_MachineChk
    {
        #region Property
          
        public string Inspect_Div_Cd { get; set; }    //검사구분
        public string Machine_Cd { get; set; }        //검사코드
        public string Machine_Nm { get; set; }        //검사명
        public int Seq { get; set; }                  //저장순번
        public int Disp_Seq { get; set; }             //표시순번
        public string Management_Item { get; set; }   //관리항목
        public string Check_Method { get; set; }      //확인방법
        public string Standard_Spec { get; set; }     //점검기준
        public string Cycle_Div_Cd { get; set; }      //관리주기
        public string Week_Div_Cd { get; set; }       //관리요일
        public string ValueType_Div_Cd { get; set; }  //등록형태
        public string Start_Date { get; set; } // 점검시작일
        public string Last_Date { get; set; } // 최종점검일
        public string Expect_Date { get; set; }  // 예상점검일
        public string Chk_Image { get; set; }         //이미지
        public string Inspect_Ok { get; set; }        //기본값
        public int Inspect_Min { get; set; }          //측정수치Min
        public int Inspect_Max { get; set; }          //측정수치Max
        public string Rmk { get; set; }               //비고

    
        #endregion Property End

        Database db = new Database();
        string query;

        public DataTable Select_Machine_Lup()
        {
            query = $@"SELECT MachineCode as 코드
                            , MachineName as 명칭 
                                                     
                         FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_Machine_Chk_Std(string Inspect_Div_Cd, bool Use_Yn = true)
        {
            query = $@"SELECT '' AS Status
                              ,mcs.Machine_Cd
                              ,mcs.Machine_Cd AS Machine_Nm
                              ,Seq
                              ,Disp_Seq
                              ,Inspect_Div_Cd
                              ,Management_Item
                              ,Check_Method
                              ,Standard_Spec
                              ,Cycle_Div_Cd
                              ,Week_Div_Cd
                              ,ValueType_Div_Cd
                              ,CASE WHEN mcs.Start_Date = '' THEN NULL ELSE CONVERT(DATE, mcs.Start_Date) END AS Start_Date
                              ,CASE WHEN mcs.Last_Date = '' THEN NULL ELSE CONVERT(DATE, mcs.Last_Date) END AS Last_Date
                              ,CASE WHEN mcs.Expect_Date = '' THEN NULL ELSE CONVERT(DATE, mcs.Expect_Date) END AS Expect_Date
                              ,Chk_Image
                              ,Inspect_Ok
                              ,Inspect_Min
	                          ,Inspect_Max
                              ,mcs.Rmk
                              ,mcs.Ins_Emp_No
                              ,mcs.Ins_Date_Time
                              ,mcs.Upd_Emp_No
                         FROM EQ_MachineChk mcs
                            LEFT JOIN BS_Machine m on mcs.Machine_Cd = m.Machine_Cd
                        WHERE mcs.Corp_Cd = '{WrGlobal.CorpID}'";

            if (Inspect_Div_Cd.ToUpper() != "ALL")
            {
                query += $"\nAND Inspect_Div_Cd = {Inspect_Div_Cd.ConvertStr_Empty_To_Null()}";
            }

            query += $"ORDER BY Machine_Cd, Disp_Seq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Machine_Chk_Std_byCd()
        {
            query = $@"
                        SELECT '' AS Status
                              ,mcs.Machine_Cd
                              ,mcs.Machine_Cd AS Machine_Nm
                              ,mcs.Seq
                              ,mcs.Disp_Seq
                              ,mcs.Inspect_Div_Cd
                              ,mcs.Management_Item
                              ,mcs.Check_Method
                              ,mcs.Standard_Spec
                              ,mcs.Cycle_Div_Cd
                              ,mcs.Week_Div_Cd
                              ,mcs.ValueType_Div_Cd
                              ,mcs.Chk_Image
                              ,mcs.Inspect_Ok
                              ,mcs.Inspect_Min
	                          ,mcs.Inspect_Max
                              ,mcs.Rmk
                              ,mcs.Ins_Emp_No
                              ,mcs.Ins_Date_Time
                              ,mcs.Upd_Emp_No
                         FROM EQ_MachineChk mcs
                            LEFT JOIN BS_Machine m on mcs.Machine_Cd = m.Machine_Cd
                         WHERE mcs.Corp_Cd = '{WrGlobal.CorpID}'
                           AND Inspect_Div_Cd = '{Inspect_Div_Cd}'
                           AND mcs.machine_Cd = '{Machine_Cd}'
                         ORDER BY Machine_Cd, Disp_Seq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Machine_Chk_List()
        {
            query = $@"SELECT  '신규' AS Status
                            , mcs.Machine_Cd
							 --, m.Machine_Nm
                             , Seq
                             , Disp_Seq
                             , NULL AS Inspect_Date
                             , ''AS Chk_Time_Div_Cd
                             , Inspect_Div_Cd
                             , Management_Item
                             , Check_Method
                             , Standard_Spec
                             , ValueType_Div_Cd
                             --, Inspect_Ok AS Inspect_Check
							 , CASE WHEN Inspect_Ok = 'Y' THEN '10'
							          WHEN Inspect_Ok = 'N' THEN '20'
									  ELSE Inspect_Ok
							   END Inspect_Check
                             , Cycle_Div_Cd
                             , Week_Div_Cd

							 , '' AS ChargeEmp_No
							 , '' AS Action_Result_Div_Cd
							 , '' AS Action_Div_Cd
							 , '' AS Action_Memo
							 , '' AS Action_Emp_No
                             
                             --, Chk_Image

                             , Inspect_Min
	                         , Inspect_Max
                             , mcs.Rmk

                         FROM EQ_MachineChk mcs
                   -- LEFT JOIN BS_Machine m on mcs.Machine_Cd = m.Machine_Cd
                        WHERE mcs.Corp_Cd = '{WrGlobal.CorpID}'
                          AND mcs.Inspect_Div_Cd = '{Inspect_Div_Cd}'
                          AND (mcs.Machine_Cd = '{Machine_Cd}'  
                                OR
								mcs.Machine_Cd = '' -- 공통항목도 보이도록
                                )
                            ";


            if (Inspect_Div_Cd == "20")
            {
                query += $"\n AND mcs.Week_Div_Cd = '{Week_Div_Cd}'";
            }

            query += "\n ORDER BY mcs.Disp_Seq";

            return db.GetDataTable(query);
        }

        public DataRow Select_Machine_Chk_Exsits()
        {
            query = $@"SELECT TOP 1 Machine_Cd
			                        ,Seq
			                        ,Inspect_Div_Cd
			                        ,CONVERT(varchar(max), Chk_Image) AS Chk_Image
                        FROM EQ_MachineChk
                        WHERE Machine_Cd = '{Machine_Cd}'
                         AND Seq = '{Seq}'
                         AND Inspect_Div_Cd = '{Inspect_Div_Cd}'
                      ";

            query += $"\nORDER BY Machine_Cd, Seq DESC";

            return db.GetDataRow(query);
        }

        public DataRow Select_Machine_Chk_Std_DispSeq()
        {
            query = $@"
                        SELECT TOP 1 Disp_Seq
                        FROM EQ_MachineChk
                        WHERE Machine_Cd = '{Machine_Cd}'
                        AND Inspect_Div_Cd = '{Inspect_Div_Cd}'
                        ORDER BY Disp_Seq DESC
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_Machine()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO EQ_MachineChk(Corp_Cd, Machine_Cd, Disp_Seq, Inspect_Div_Cd, Management_Item, Check_Method, Standard_Spec,
                                                Cycle_Div_Cd, Week_Div_Cd, ValueType_Div_Cd, Inspect_Ok, Inspect_Min, Inspect_Max, Rmk,
                                                Ins_Emp_No, Ins_Date_Time
                                                )
                                        VALUES ('{WrGlobal.CorpID}'
                                                , '{Machine_Cd}'
                                                , '{Disp_Seq}'
                                                , '{Inspect_Div_Cd}'
                                                , '{Management_Item}'
                                                , '{Check_Method}'
                                                , '{Standard_Spec}'
                                                , '{Cycle_Div_Cd}'
                                                , '{Week_Div_Cd}'
                                                , '{ValueType_Div_Cd}'
                                                , '{Inspect_Ok}'
                                                , '{Inspect_Min}'
                                                , '{Inspect_Max}'
                                                , '{Rmk}'
                                                ,'{WrGlobal.LoginID}'
                                                ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Machine()
        {
            // Inspect_Div_Cd
            db.Edit_Property(this);

            query = $@"UPDATE EQ_MachineChk
                          SET 
                             Machine_Cd =            '{Machine_Cd}'
                            , Disp_Seq  =              '{Disp_Seq}'
                            , Inspect_Div_Cd =         '{Inspect_Div_Cd}'
                            , Management_Item =        '{Management_Item}'
                            , Check_Method =           '{Check_Method}'
                            , Standard_Spec =          '{Standard_Spec}'
                            , Cycle_Div_Cd =           '{Cycle_Div_Cd}'
                            , Week_Div_Cd =            '{Week_Div_Cd}'
                            , ValueType_Div_Cd =       '{ValueType_Div_Cd}'
                            , Inspect_Ok =             '{Inspect_Ok}'
                            , Inspect_Min =            '{Inspect_Min}'
                            , Inspect_Max =            '{Inspect_Max}'
                            , Rmk =                    '{Rmk}'
                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()

                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Seq = '{Seq}'
						  --AND Disp_Seq = '{Disp_Seq}'";

            return db.ExecuteQuery(query);
        }
        public bool Upd_Machine_Chk_Image()
        {
            // Exam_Div_Cd
            db.Edit_Property(this);

            query = $@"UPDATE EQ_MachineChk
                          SET Chk_Image = CONVERT(varbinary(max),{Chk_Image.ConvertStr_Empty_To_Null()})
    
                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()

                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Machine_Cd = '{Machine_Cd}'
                          AND Seq = '{Seq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Machine()
        {
            query = $@"DELETE EQ_MachineChk
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Machine_Cd = '{Machine_Cd}'
                          AND Seq = '{Seq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_InspectResult()
        {
            query = $@"DELETE EQ_Machine_Chk
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
						  AND Machine_Cd = '{Machine_Cd}'
						  AND Seq = '{Seq}' ";

            return db.ExecuteQuery(query);
        }
    }
}
