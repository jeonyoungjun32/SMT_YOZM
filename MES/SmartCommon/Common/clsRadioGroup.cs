using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System;
using System.Data;

namespace SmartCommon.Common
{
    public class clsRadioGroup
    {
        /// <summary>
        /// RadioGroup Item 생성 / 단일데이터 입력
        /// </summary>
        /// <param name="radioGroup">입력할 RadioGroup</param>
        /// <param name="description">표시할 명칭</param>
        /// <param name="value">데이터 값</param>
        public void Create_RadioGroupItem(RadioGroup radioGroup, string description, string value)
        {
            RadioGroupItem rdoItem = new RadioGroupItem
            {
                Description = description, //표기할 명칭
                Value = value              //데이터 값
            };

            radioGroup.Properties.Items.Add(rdoItem);
        }

        /// <summary>
        /// RadioGroup Item 생성 / 테이블 입력
        /// </summary>
        /// <param name="radioGroup">입력할 RadioGroup</param>
        /// <param name="dt">입력할 데이터 리스트</param>
        /// <param name="description">표시할 Column 명칭</param>
        /// <param name="value">데이터 값 Column 명칭</param>
        public void Create_RadioGroupItem(RadioGroup rdo, DataTable dt, string description, string value)
        {
            foreach (DataRow dr in dt.Rows)
            {
                RadioGroupItem rdoItem = new RadioGroupItem
                {
                    Description = Convert.ToString(dr[description]), //표기할 명칭
                    Value = Convert.ToString(dr[value])              //데이터 값
                };

                rdo.Properties.Items.Add(rdoItem);
            }

            //길이 조정
            rdo.Width = 0;
            foreach (RadioGroupItem item in rdo.Properties.Items)
            {
                rdo.Width += (item.Description.Length * 20) + 25;
            }

            //초기값 첫번째 버튼으로 설정
            rdo.SelectedIndex = 0;
        }

        /// <summary>
        /// RadioGroup Item 생성 Repository Ver
        /// </summary>
        /// <param name="radioGroup">입력할 RadioGroup</param>
        /// <param name="description">표시할 명칭</param>
        /// <param name="value">데이터 값</param>
        public void Create_RadioGroupItem(RepositoryItemRadioGroup radioGroup, string description, string value)
        {
            RadioGroupItem rdoItem = new RadioGroupItem
            {
                Description = description, //표기할 명칭
                Value = value              //데이터 값
            };

            radioGroup.Items.Add(rdoItem);
        }
    }
}
