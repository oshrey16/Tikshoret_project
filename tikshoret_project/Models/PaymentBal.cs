using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tikshoret_project.Models
{
    //TODO!!!
    public class PaymentBal
    {
        /*=====ID===== */
        [Key]
        [Required(ErrorMessage = "Enter ID!!")]
        [Display(Name = "ID")]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "ID is not valid - 9 digits")]
        public string Id { get; set; }
        /*=====Credit Card===== */
        [Required(ErrorMessage = "Enter Credit Card Number!!")]
        [Display(Name = "Credit Card")]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Credit Card Number is not valid - 16 digits!")]
        public string CreditCardNum { get; set; }
        /*=====Expiration Date===== */
        [Required(ErrorMessage = "Enter Expiration Date!!")]
        [RegularExpression(@"(0[1-9]|1[0-2])/([2-3][1-9])", ErrorMessage = "Credit Card Number is not valid format: MM/YY or card expired")]
        [Display(Name = "Expiration Date")]
        public string ExpDate { get; set; }
        /*=====cvv===== */
        [Required(ErrorMessage = "Enter CVV!!")]
        [Display(Name = "CVV")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "Please Enter 3 digits CVV")]
        public string Cvv { get; set; }
    }
}