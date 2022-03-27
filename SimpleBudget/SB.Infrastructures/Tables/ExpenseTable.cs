using SQLite;

namespace SB.Infrastructures.Tables
{
    /// <summary>
    /// 支出テーブル
    /// </summary>
    public class ExpenseTable
    {
        /// <summary>
        /// 支出ID
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// 支出年月日
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Memo { get; set; } = "";

        /// <summary>
        /// 金額
        /// </summary>
        public int Yen { get; set; }

        /// <summary>
        /// 作成日付
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日付
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
