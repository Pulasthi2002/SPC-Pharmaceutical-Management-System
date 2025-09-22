using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPCPharmacyManagement.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(255, ErrorMessage = "Company name cannot exceed 255 characters")]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(255)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(50)]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "License number is required")]
        [StringLength(100)]
        public string LicenseNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        public Supplier()
        {
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }
    }

    public class Drug
    {
        public int DrugId { get; set; }

        [Required(ErrorMessage = "Drug name is required")]
        [StringLength(255)]
        public string DrugName { get; set; }

        [StringLength(255)]
        public string GenericName { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        [StringLength(255)]
        public string Manufacturer { get; set; }

        [StringLength(100)]
        public string BatchNumber { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int QuantityInStock { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MinimumStockLevel { get; set; }
        public int MaximumStockLevel { get; set; }

        public Drug()
        {
            CreatedDate = DateTime.Now;
            MinimumStockLevel = 10;
            MaximumStockLevel = 1000;
        }

        // Helper properties
        public bool IsLowStock => QuantityInStock <= MinimumStockLevel;
        public bool IsExpired => ExpiryDate <= DateTime.Now;
        public bool IsNearExpiry => ExpiryDate <= DateTime.Now.AddMonths(3);
        public string StockStatus => IsLowStock ? "Low Stock" : IsExpired ? "Expired" : IsNearExpiry ? "Near Expiry" : "Normal";
    }

    public class Pharmacy
    {
        public int PharmacyId { get; set; }

        [Required(ErrorMessage = "Pharmacy name is required")]
        [StringLength(255)]
        public string PharmacyName { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [Phone]
        [StringLength(50)]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "License number is required")]
        [StringLength(100)]
        public string LicenseNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public string PharmacyType { get; set; } // "SPC_OWNED", "LINKED_DEALER"

        public Pharmacy()
        {
            RegistrationDate = DateTime.Now;
            IsActive = true;
            PharmacyType = "LINKED_DEALER";
        }
    }

    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public int PharmacyId { get; set; }

        public string PharmacyName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public string OrderNotes { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;
            Status = "PENDING";
            PaymentStatus = "PENDING";
        }

        // Helper properties
        public bool IsPending => Status == "PENDING";
        public bool IsProcessing => Status == "PROCESSING";
        public bool IsShipped => Status == "SHIPPED";
        public bool IsDelivered => Status == "DELIVERED";
        public bool IsCancelled => Status == "CANCELLED";
        public int TotalItems => OrderItems?.Count ?? 0;
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int DrugId { get; set; }

        public string DrugName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }

        public OrderItem()
        {
            DiscountPercentage = 0;
            DiscountAmount = 0;
        }

        public void CalculateTotalPrice()
        {
            var subtotal = Quantity * UnitPrice;
            DiscountAmount = subtotal * (DiscountPercentage / 100);
            TotalPrice = subtotal - DiscountAmount;
        }

        public void SetTotalPrice()
        {
            TotalPrice = Quantity * UnitPrice;
        }
    }

    public class StockUpdate
    {
        public int StockUpdateId { get; set; }

        [Required]
        public int DrugId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string UpdateType { get; set; } // "ADD", "REMOVE", "ADJUSTMENT"

        public string Reason { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        [Required]
        [StringLength(255)]
        public string UpdatedBy { get; set; }

        public string ReferenceNumber { get; set; }

        public StockUpdate()
        {
            UpdateDate = DateTime.Now;
        }
    }

    public class ManufacturingPlant
    {
        public int PlantId { get; set; }

        [Required]
        [StringLength(255)]
        public string PlantName { get; set; }

        [Required]
        public string Location { get; set; }

        [StringLength(50)]
        public string PlantCode { get; set; }

        public string Manager { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string SystemType { get; set; } // "WEB_BASED", "WINDOWS_BASED"

        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public DateTime EstablishedDate { get; set; }

        public ManufacturingPlant()
        {
            IsActive = true;
            EstablishedDate = DateTime.Now;
        }
    }

    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // "ADMIN", "MANAGER", "OPERATOR", "VIEWER"

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public string Department { get; set; }
        public string Phone { get; set; }

        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        // Helper properties
        public bool IsAdmin => Role == "ADMIN";
        public bool IsManager => Role == "MANAGER";
        public string DisplayName => !string.IsNullOrEmpty(FullName) ? FullName : Username;
    }

    // Add these new classes to your existing Models.cs file

    public class Tender
    {
        public int TenderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public List<TenderItem> Items { get; set; } = new List<TenderItem>();
    }

    public class TenderItem
    {
        public int TenderItemId { get; set; }
        public int TenderId { get; set; }
        public string DrugName { get; set; }
        public int RequiredQuantity { get; set; }
    }

    public class TenderProposal
    {
        public int ProposalId { get; set; }
        public int TenderId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } // For display purposes
        public DateTime ProposalDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public decimal TotalProposedAmount { get; set; }
        public List<ProposalItem> Items { get; set; } = new List<ProposalItem>();
    }

    public class ProposalItem
    {
        public int ProposalItemId { get; set; }
        public int ProposalId { get; set; }
        public int TenderItemId { get; set; }
        public string DrugName { get; set; } // For display purposes
        public int RequiredQuantity { get; set; } // For display purposes
        public decimal ProposedUnitPrice { get; set; }
        public decimal TotalPrice => RequiredQuantity * ProposedUnitPrice;
    }

}
