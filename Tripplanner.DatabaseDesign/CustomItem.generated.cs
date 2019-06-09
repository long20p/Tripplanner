//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//     Produced by Entity Framework Visual Editor
//     https://github.com/msawczyn/EFDesigner
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Tripplanner.DatabaseDesign
{
   public partial class CustomItem
   {
      partial void Init();

      /// <summary>
      /// Default constructor. Protected due to required properties, but present because EF needs it.
      /// </summary>
      protected CustomItem()
      {
         Init();
      }

      /// <summary>
      /// Public constructor with required data
      /// </summary>
      /// <param name="key"></param>
      /// <param name="value"></param>
      /// <param name="_trip0"></param>
      public CustomItem(string key, string value, global::Tripplanner.DatabaseDesign.Trip _trip0)
      {
         if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
         this.Key = key;
         if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
         this.Value = value;
         if (_trip0 == null) throw new ArgumentNullException(nameof(_trip0));
         _trip0.CustomItems.Add(this);

         Init();
      }

      /// <summary>
      /// Static create function (for use in LINQ queries, etc.)
      /// </summary>
      /// <param name="key"></param>
      /// <param name="value"></param>
      /// <param name="_trip0"></param>
      public static CustomItem Create(string key, string value, global::Tripplanner.DatabaseDesign.Trip _trip0)
      {
         return new CustomItem(key, value, _trip0);
      }

      /*************************************************************************
       * Persistent properties
       *************************************************************************/

      /// <summary>
      /// Identity, Required, Indexed
      /// </summary>
      [Key]
      [Required]
      public int Id { get; set; }

      /// <summary>
      /// Required
      /// </summary>
      [Required]
      public string Key { get; set; }

      /// <summary>
      /// Required
      /// </summary>
      [Required]
      public string Value { get; set; }

      /*************************************************************************
       * Persistent navigation properties
       *************************************************************************/

   }
}

