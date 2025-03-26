﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManagement.Models
{
    /// <summary>
    /// Klasa reprezentująca narzędzie.
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// Identyfikator narzędzia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa narzędzia.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ilość narzędzi.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Cena narzędzia.
        /// </summary>
        public decimal Price { get; set; }
    }


}
