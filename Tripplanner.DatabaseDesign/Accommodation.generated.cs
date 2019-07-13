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
   public partial class Accommodation
   {
      partial void Init();

      /// <summary>
      /// Default constructor. Protected due to required properties, but present because EF needs it.
      /// </summary>
      protected Accommodation()
      {
         Init();
      }

      /// <summary>
      /// Public constructor with required data
      /// </summary>
      /// <param name="address"></param>
      /// <param name="_trip0"></param>
      public Accommodation(string address, global::Tripplanner.DatabaseDesign.Trip _trip0)
      {
         if (string.IsNullOrEmpty(address)) throw new ArgumentNullException(nameof(address));
         this.Address = address;
         if (_trip0 == null) throw new ArgumentNullException(nameof(_trip0));
         _trip0.Accommodations.Add(this);

         Init();
      }

      /// <summary>
      /// Static create function (for use in LINQ queries, etc.)
      /// </summary>
      /// <param name="address"></param>
      /// <param name="_trip0"></param>
      public static Accommodation Create(string address, global::Tripplanner.DatabaseDesign.Trip _trip0)
      {
         return new Accommodation(address, _trip0);
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
      public string Address { get; set; }

      public byte[] ReservationPhoto { get; set; }

      public DateTime? From { get; set; }

      public DateTime? To { get; set; }

      /*************************************************************************
       * Persistent navigation properties
       *************************************************************************/

   }
}

