using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace transactR.Core.Entities
{
    public interface IEntity
    {
        [Key]
        [Editable(false)]
        int Id
        {
            get;
            set;
        }

        string CreatedBy
        {
            get;
            set;
        }

        DateTime CreatedOn
        {
            get;
            set;
        }

        DateTime? LastUpdatedOn
        {
            get;
            set;
        }
    }
}
