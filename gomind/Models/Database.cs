using IdentitySample.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gomind.Models
{
    public class Comment_Member
    {
        [Key]
        public int id{ get; set; }
        public string tousername { get; set; }
        public string touserid { get; set; }
        public string ccont { get; set; }
        public string score { get; set; }
        public string type { get; set; }
        public DateTime? addtime { get; set; }
        public bool edit { get; set; }

        public virtual ApplicationUser User { get; set; }


        public virtual Order Order { get; set; }

    }
    public class Comment_Product
    {
        [Key]
        public int number { get; set; }


        [Required]
        [StringLength(256)]
        [DisplayName("內容")]
        public string prccont { get; set; }

    
        [Required]
        [DisplayName("時間")]
        public DateTime prctime { get; set; }

        public virtual ProductList ProductList { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

    public class FollowTable
    {
        [Key]
        public int number { get; set; }
       
        public virtual ApplicationUser User { get; set; }
        public virtual ProductList ProductList { get; set; }
    }

    public class FollowUser
    {
        [Key]
        public int number { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
 
        public string followuserid { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class ProductList
    {
        [Key]
        public System.Guid number { get; set; }

        [StringLength(256)]
        public string userid { get; set; }

        [Required]
        [DisplayName("賣家")]
        public string plistsale_mnumber { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("商品狀態")]
        public string prodlist_status { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("商品名稱")]
        public string prodlist_name { get; set; }

        [Required]
        [DisplayName("價格")]
        public int prodlist_price { get; set; }

        [StringLength(256)]
        [DisplayName("商品敘述")]
        public string prodlist_content { get; set; }

        [Required]
        [DisplayName("數量")]
        public int count { get; set; }

        [DisplayName("建立時間")]
        public System.DateTime createdate { get; set; }

        [DisplayName("商品讚數")]
        public int? prodlist_followtimes { get; set; }

        [DisplayName("商品種類")]
        public string onekind { get; set; }

        public string twokind { get; set; }

        public string url { get; set; }

        public virtual ICollection<Comment_Product> Comment_Product { get; set; }
        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<FollowTable> FollowTable { get; set; }
        public virtual ICollection<Chat> Chat { get; set; }
        public virtual ApplicationUser User { get; set; }

    }

    public class File
    {
        [Key]
        public int FileId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public byte[] Content { get; set; }
        public int number { get; set; }

        public virtual ProductList ProductList { get; set; }
    }
    public class UserData 
    {
        [Key]
        public string id { get; set; }
        [StringLength(255)]
        [DisplayName("暱稱")]
        public string Nickname { get; set; }
        [DisplayName("生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime? Birthday { get; set; }
        [StringLength(255)]
        [DisplayName("性別")]
        public string Sex { get; set; }
        public int? number { get; set; }
        public string content { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

    public class UserFile
    {
        [Key]
        public int FileId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public byte[] Content { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }

    public class SaleSetting
    {

        [Key]
        public int id { get; set; }
        [DisplayName("面交")]
        public bool SendFace { get; set; }
       
        [DisplayName("宅配")]
        public bool SendHome { get; set; }
        [DisplayName("7-11取貨付款")]
        public bool SendSeven { get; set; }
        [DisplayName("全家取貨付款")]
        public bool SendFamily { get; set; }
        [DisplayName("郵局寄送")]
        public bool SendPost { get; set; }
        [DisplayName("宅配")]
        public int? HomeMoney { get; set; }
        [DisplayName("7-11")]
        public int? SevenMoney { get; set; }
        [DisplayName("全家")]
        public int? FamilMoney { get; set; }
        [DisplayName("郵局")]
        public int? PostMoney { get; set; }


        public virtual ApplicationUser User { get; set; }
    }

   
    public class Setting
    {
        [Key]
        public int id { get; set; }
 
        
        public int OrderNumber { get; set; }
        public int Talk { get; set; }
   
        public int TalkNumber { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class Order
    {
        [Key]
        public int id { get; set; }
        public string buyerid { get; set; }
        [DisplayName("買家姓名")]
        public string name { get; set; }
        [DisplayName("建立時間")]
        public DateTime? createtime { get; set; }
        [DisplayName("是否付款")]
        public bool pay { get; set; }
        [DisplayName("是否出貨")]
        public bool send { get; set; }
        public bool getco { get; set; }
        public bool givco { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual ICollection<Comment_Member> Comment_Member { get; set; }
    }

    public class One
    {
        [Key]
        public int OneId { get; set; }
        public string OneKind { get; set; }
    }
    public class Two
    {
        [Key]
        public int Id { get; set; }
        public int OneId { get; set; }
        public string TwoKind { get; set; }
    }



    public  class Chat
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string toUserId { get; set; }
        public string toUserName { get; set; }
        public int count { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ChatMessage> ChatMessage { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ProductList ProductList { get; set; }
    }

    public  class ChatMessage
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime addtime { get; set; }

        public virtual Chat Chat { get; set; }
    }
 
}