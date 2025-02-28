namespace EasyBlog.Core.Entities;

public abstract class EntityBase : IEntityBase
{
    // Tüm string propertyleri otomatik olarak "" (string.Empty) yap
    protected EntityBase()
    {
        /* Nasıl Çalışıyor ??
         * GetType().GetProperties() ile sınıftaki tüm propertyleri alıyoruz.
         * Eğer bir property string türündeyse ve değeri null ise, string.Empty atıyoruz.
         * Böylece her entity için tek tek string.Empty yazmaya gerek kalmıyor.
         */

        foreach (var prop in GetType().GetProperties())
            if (prop.PropertyType == typeof(string) && prop.GetValue(this) == null)
                prop.SetValue(this, string.Empty);
    }


    // Ezme işlemi yapmayacağım için virtual kullanmadım

    public Guid Id { get; set; } = Guid.NewGuid();

    public string CreatedBy { get; set; } = default!;
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; private set; } = false;


    public void MarkAsDeleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedBy = deletedBy;
        DeletedDate = DateTime.UtcNow;
    }
}