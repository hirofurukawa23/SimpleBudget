using SB.Domain.ValueObjects;

namespace SB.Domain.Entities
{
    public class Expense : ICloneable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memo"></param>
        /// <param name="yen"></param>
        public Expense(Date date, Memo memo, Yen yen, int? id = null, DateTime? created = null, DateTime? updated = null)
        {
            Id = id;
            Date = date;
            Memo = memo;
            Yen = yen;
            CreatedAt = created;
            UpdatedAt = updated;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; }

        /// <summary>
        /// 日付
        /// </summary>
        public Date Date { get; }

        /// <summary>
        /// 備考
        /// </summary>
        public Memo Memo { get; }

        /// <summary>
        /// 金額
        /// </summary>
        public Yen Yen { get; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? CreatedAt { get; }

        /// <summary>
        /// 表示用登録日
        /// 例：2022/03/22
        /// </summary>
        public string CreatedAtWithSlash
            => CreatedAt is null ? "" : $"{CreatedAt.Value.Year}/{CreatedAt.Value.Month.ToString("00")}/{CreatedAt.Value.Day.ToString("00")}";

        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpdatedAt { get; }

        /// <summary>
        /// 表示用更新日
        /// 例：2022/03/22
        /// </summary>
        public string UpdatedAtWithSlash
            => UpdatedAt is null ? "" : $"{UpdatedAt.Value.Year}/{UpdatedAt.Value.Month.ToString("00")}/{UpdatedAt.Value.Date.ToString("00")}";

        /// <summary>
        /// 別インスタンスでコピーする
        /// </summary>
        /// <returns></returns>
        public Object Clone()
        {
            return new Expense(Date, Memo, Yen, Id);
        }
    }
}
