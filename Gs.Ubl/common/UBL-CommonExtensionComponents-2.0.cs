// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//
//     http://ubllarsen.codeplex.com/
//
// </auto-generated>
//------------------------------------------------------------------------------
namespace Gs.Ubl.v2.Ext
{
    using Udt;
    using Sac;
    
    
    /// <summary>
    ///          A single extension for private use.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class UBLExtensionType
    {
        
        private Udt.IdentifierType idField;
        
        private Udt.NameType nameField;
        
        private ExtensionAgencyIDType extensionAgencyIDField;
        
        private ExtensionAgencyNameType extensionAgencyNameField;
        
        private ExtensionVersionIDType extensionVersionIDField;
        
        private ExtensionAgencyURIType extensionAgencyURIField;
        
        private ExtensionURIType extensionURIField;
        
        private ExtensionReasonCodeType extensionReasonCodeField;
        
        private ExtensionReasonType extensionReasonField;

        private AdditionalsInformationType extensionContentField;
        
        /// <summary>
        ///  An identifier for the Extension assigned by the creator of the extension.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.IdentifierType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <summary>
        ///  A name for the Extension assigned by the creator of the extension.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.NameType Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <summary>
        ///              An agency that maintains one or more Extensions.
        /// </summary>
        public ExtensionAgencyIDType ExtensionAgencyID
        {
            get
            {
                return this.extensionAgencyIDField;
            }
            set
            {
                this.extensionAgencyIDField = value;
            }
        }
        
        /// <summary>
        ///              The name of the agency that maintains the Extension.
        /// </summary>
        public ExtensionAgencyNameType ExtensionAgencyName
        {
            get
            {
                return this.extensionAgencyNameField;
            }
            set
            {
                this.extensionAgencyNameField = value;
            }
        }
        
        /// <summary>
        ///              The version of the Extension.
        /// </summary>
        public ExtensionVersionIDType ExtensionVersionID
        {
            get
            {
                return this.extensionVersionIDField;
            }
            set
            {
                this.extensionVersionIDField = value;
            }
        }
        
        /// <summary>
        ///              A URI for the Agency that maintains the Extension.
        /// </summary>
        public ExtensionAgencyURIType ExtensionAgencyURI
        {
            get
            {
                return this.extensionAgencyURIField;
            }
            set
            {
                this.extensionAgencyURIField = value;
            }
        }
        
        /// <summary>
        ///              A URI for the Extension.
        /// </summary>
        public ExtensionURIType ExtensionURI
        {
            get
            {
                return this.extensionURIField;
            }
            set
            {
                this.extensionURIField = value;
            }
        }
        
        /// <summary>
        ///              A code for reason the Extension is being included.
        /// </summary>
        public ExtensionReasonCodeType ExtensionReasonCode
        {
            get
            {
                return this.extensionReasonCodeField;
            }
            set
            {
                this.extensionReasonCodeField = value;
            }
        }
        
        /// <summary>
        ///              A description of the reason for the Extension.
        /// </summary>
        public ExtensionReasonType ExtensionReason
        {
            get
            {
                return this.extensionReasonField;
            }
            set
            {
                this.extensionReasonField = value;
            }
        }
        
        /// <summary>
        /// The definition of the extension content.
        /// </summary>
        public AdditionalsInformationType ExtensionContent
        {
            get
            {
                return this.extensionContentField;
            }
            set
            {
                this.extensionContentField = value;
            }
        }
    }
    
    /// <summary>
    ///  A character string to identify and distinguish uniquely, one instance of an object in an identification scheme from all other objects in the same scheme together with relevant supplementary information.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionAgencyIDType : IdentifierType
    {
    }
    
    /// <summary>
    ///  A character string (i.e. a finite set of characters) generally in the form of words of a language.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionAgencyNameType : TextType
    {
    }
    
    /// <summary>
    ///  A character string to identify and distinguish uniquely, one instance of an object in an identification scheme from all other objects in the same scheme together with relevant supplementary information.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionVersionIDType : IdentifierType
    {
    }
    
    /// <summary>
    ///  A character string to identify and distinguish uniquely, one instance of an object in an identification scheme from all other objects in the same scheme together with relevant supplementary information.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionAgencyURIType : IdentifierType
    {
    }
    
    /// <summary>
    ///  A character string to identify and distinguish uniquely, one instance of an object in an identification scheme from all other objects in the same scheme together with relevant supplementary information.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionURIType : IdentifierType
    {
    }
    
    /// <summary>
    ///  A character string (letters, figures, or symbols) that for brevity and/or languange independence may be used to represent or replace a definitive value or text of an attribute together with relevant supplementary information.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionReasonCodeType : CodeType
    {
    }
    
    /// <summary>
    ///  A character string (i.e. a finite set of characters) generally in the form of words of a language.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
    public partial class ExtensionReasonType : TextType
    {
    }
}
