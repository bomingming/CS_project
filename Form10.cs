﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMP_cs
{
    public partial class Form10 : Form
    {
        DB_connect dB_Connect;
        DataTable table;
        string sqlQuery = ""; // sqlQuery문을 담을 문자열
        
        string tg_companyID; // 선택된 회사ID를 담을 문자열
        string tg_companyName; // 선택된 회사명을 담을 문자열
        Form4 frm4;

        public Form10(Form4 form4)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            frm4 = form4;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            dB_Connect = new DB_connect();
            dB_Connect.Open();

            sqlQuery = "SELECT * FROM `Company` ORDER BY `CompanyID` ASC";

            try
            {
                table = dB_Connect.readSQL(sqlQuery);
                table.Columns[0].ColumnName = "회사ID";
                table.Columns[1].ColumnName = "회사명";
                table.Columns[2].ColumnName = "회사번호";

                dataGridView1.DataSource = table;

                // DataGridView 열 색상 변경
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Lavender;

                // 중복 선택 불가
                dataGridView1.MultiSelect = false;

                // 행 단위로 클릭
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // DataGridView 첫 번째 열 출력하지 않기 
                dataGridView1.RowHeadersVisible = false;

                // 목록과 DataGridView 크기 맞추기
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dB_Connect.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11(this);
            form11.ShowDialog();
        }

        public void Update_DB()
        {
            dB_Connect = new DB_connect();
            dB_Connect.Open();

            sqlQuery = "SELECT * FROM `Company` ORDER BY `CompanyID` ASC";

            try
            {

                table.Clear();
                table.Columns.Clear();

                table = dB_Connect.readSQL(sqlQuery);
                table.Columns[0].ColumnName = "회사ID";
                table.Columns[1].ColumnName = "회사명";
                table.Columns[2].ColumnName = "회사번호";

                dataGridView1.DataSource = table;

                // DataGridView 열 색상 변경
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Lavender;

                // 중복 선택 불가
                dataGridView1.MultiSelect = false;

                // 행 단위로 클릭
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // DataGridView 첫 번째 열 출력하지 않기 
                dataGridView1.RowHeadersVisible = false;

                // 목록과 DataGridView 크기 맞추기
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                

                dB_Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dB_Connect = new DB_connect();
            dB_Connect.Open();


            DataGridViewRow selectRow = dataGridView1.SelectedRows[0]; // 선택된 행
            tg_companyID = selectRow.Cells[0].Value.ToString(); // 선택된 행의 회사ID 값
            tg_companyName = selectRow.Cells[1].Value.ToString();

            if ((MessageBox.Show("해당 업체를 삭제하시겠습니까?", "확인창", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                try
                {
                    sqlQuery = $"DELETE FROM Company WHERE Company.CompanyID = '{tg_companyID}'";
                    dB_Connect.SQLQuery(sqlQuery);

                    frm4.change_textBox2("");
                    Update_DB();

                    MessageBox.Show($"'{tg_companyName}'가 삭제되었습니다.", "삭제 완료");
                    
                    dB_Connect.Close();
                    this.Close();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string Chan_val = row.Cells[1].Value.ToString();

            frm4.change_textBox2(Chan_val);
            this.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string Chan_val = row.Cells[1].Value.ToString();

            frm4.change_textBox2(Chan_val);
            this.Close();
        }
    }
}
