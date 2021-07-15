using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tikshoret_project.Models
{
    public class AdminBAL
    {
        /*=====ID===== */
        [Key]
        [Required(ErrorMessage = "ERROR ID!!")]
        [Display(Name = "ID")]
        [RegularExpression(@"[0-9]{5}", ErrorMessage = "ID is not valid")]
        public string Id { get; set; }
        /*=====email===== */
        [Required(ErrorMessage = "ERROR EMAIL!!")]
        [Display(Name = "Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        /*=====pass===== */
        [Required(ErrorMessage = "ERROR password!!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "You Need Minimum Length 8")]
        public string Pass { get; set; }
        /*=====cname===== */
        [Required(ErrorMessage = "ERROR Cname!!")]
        [Display(Name = "Name")]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Cname { get; set; }
        /*=====caddress===== */
        [Required(ErrorMessage = "ERROR Address!!")]
        [Display(Name = "Address")]
        public string Caddress { get; set; }
        /*=====bdate===== */
        [Required(ErrorMessage = "ERROR bdate!!")]
        [Display(Name = "bdate")]
        public Nullable<System.DateTime> bdate { get; set; }
        /*=====phone===== */
        [Required(ErrorMessage = "ERROR phone!!")]
        [Display(Name = "phone")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "phone is not valid")]
        public string Phone { get; set; }
    }
}