﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardGame.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CardGame.Resources.ValidationMessages", typeof(ValidationMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bitte eine gültige Emailadresse eingeben.
        /// </summary>
        public static string EMAIL {
            get {
                return ResourceManager.GetString("EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bitte mindestens 4, höchstens 50 Buchstaben.
        /// </summary>
        public static string LENGTH {
            get {
                return ResourceManager.GetString("LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Du hast leider die maximale Wortlänge überschritten.
        /// </summary>
        public static string MAX_LENGTH {
            get {
                return ResourceManager.GetString("MAX_LENGTH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die Passwörter müssen gleich sein.
        /// </summary>
        public static string PASSWORD_EQUAL {
            get {
                return ResourceManager.GetString("PASSWORD_EQUAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bitte Feld unbedingt ausfüllen.
        /// </summary>
        public static string REQUIRED {
            get {
                return ResourceManager.GetString("REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bitte keine Sonderzeichen benutzen.
        /// </summary>
        public static string SPECIAL_CHARACTER {
            get {
                return ResourceManager.GetString("SPECIAL_CHARACTER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bitte das Kästchen anhacken.
        /// </summary>
        public static string TERMS {
            get {
                return ResourceManager.GetString("TERMS", resourceCulture);
            }
        }
    }
}
