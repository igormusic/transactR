using System;
using System.ComponentModel.DataAnnotations;

namespace TransactRules.Core.Entities
{
    public abstract class Entity:IEntity
    {
        public Entity()
        {
            CreatedOn = DateTime.Now;
        }

        [Key]
        [Editable(false)]
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string CreatedBy
        {
            get;
            set;
        }

        public virtual DateTime CreatedOn
        {
            get;
            set;
        }

        public virtual DateTime? LastUpdatedOn
        {
            get;
            set;
        }

        public virtual int? Version { get; set; }
    }
}
