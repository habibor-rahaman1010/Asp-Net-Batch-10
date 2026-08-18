using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="BarcodeType"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class BarcodeTypeConfiguration : IEntityTypeConfiguration<BarcodeType>
    {
        public void Configure(EntityTypeBuilder<BarcodeType> builder)
        {

            builder.HasData(
                new BarcodeType { Id = new Guid("03d28362-1525-4dfb-b46b-4656bd76dd1f"), BarcodeTypeName = "QR Code" },
                new BarcodeType { Id = new Guid("c43bf678-8caf-46a8-900c-64e58c67b1fb"), BarcodeTypeName = "UPC" },
                new BarcodeType { Id = new Guid("7da178ba-0a11-4836-a55d-93ee28bfcdb1"), BarcodeTypeName = "NFC" }
            );
        }
    }
}
