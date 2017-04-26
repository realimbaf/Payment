using System;

namespace Wiki.Payment.Common.POCO
{
    public class Manager
    {
        public int Id { get; private set; }
        public string Phone { get; private set; }
        public string Name { get; private set; }
        public DateTime? LastModified { get; private set; }

        public Manager(int id,string phone,string name)
        {
            this.Id = id;
            this.Phone = phone;
            this.Name = name;
        }

        public Manager(int id, string phone,string name, DateTime? lastModified)
        {
            this.Id = id;
            this.Phone = phone;
            this.Name = name;
            LastModified = lastModified;
        }

    }
}
