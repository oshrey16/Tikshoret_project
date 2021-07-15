using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tikshoret_project.Models
{
    //TODO!!!
    public class AccountBAL
    {
        /*=====ID===== */
        [Key]
        [Required(ErrorMessage = "Enter ID!!")]
        [Display(Name = "ID")]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "ID is not valid - 9 digits")]
        public string Id { get; set; }
        /*=====email===== */
        [Required(ErrorMessage = "Enter EMAIL!!")]
        [Display(Name = "Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        /*=====pass===== */
        [Required(ErrorMessage = "Enter password!!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100,MinimumLength =4,ErrorMessage ="You Need Minimum Length 4")]
        public string Pass { get; set; }
        /*=====cname===== */
        [Required(ErrorMessage = "Enter Cname!!")]
        [Display(Name = "Name")]
        [RegularExpression(@"[a-zA-Z]+ [a-zA-Z]+", ErrorMessage = "Please Enter Full Name")]
        public string Cname { get; set; }
        /*=====caddress===== */
        [Required(ErrorMessage = "Enter Address!!")]
        [Display(Name = "Address")]
        public string Caddress { get; set; }
        /*=====bdate===== */
        [Required(ErrorMessage = "Enter bdate!!")]
        [Display(Name = "bdate")]
        public Nullable<System.DateTime> bdate { get; set; }
        /*=====phone===== */
        [Required(ErrorMessage = "Enter phone!!")]
        [Display(Name = "phone")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "phone is not valid")]
        public string Phone { get; set; }
    }
}