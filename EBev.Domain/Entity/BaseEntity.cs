namespace EBev.Domain
{
    public abstract class BaseEntity : IDisposable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }

        private bool disposedValue = false;
        public BaseEntity()
        {
            this.CreatedOn = DateTime.Now;
            this.IsActive = true;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}