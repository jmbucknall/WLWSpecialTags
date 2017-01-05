using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace WLWSpecialTags {

  [WriterPlugin("B8D71B52-C57C-425c-A2DC-37B9F08305C4", "Special Tag Inserter")]
  [InsertableContentSource("Special Tag/Entity")]
  public class SpecialTagInserter : ContentSource {

    Dictionary<string, string> EntityMap;

    public SpecialTagInserter()
      : base() {
      CreateEntityMap();
    }
    public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content) {

      // The different types of content and how they're converted:
      // `something` => <code>something</code>
      // something => an entity character, given by the EntityMap below
      // otherwise, just returns the content wrapped in "&" and ";"

      string trimmedContent = "";
      DialogResult result = DialogResult.Cancel;

      if (HasContent(content, ref trimmedContent)) { 
        if (IsValidMarkdownCode(trimmedContent)) {
          content = Embed(GetMiddle(trimmedContent), "code");
        }
        else {
          content = ConvertEntity(trimmedContent);
        }
        result = DialogResult.OK;
      }
      return result;
    }

    private static bool IsFirstChar(string s, char c) {
      return s[0] == c;
    }
    private static bool IsLastChar(string s, char c) {
      return s[s.Length - 1] == c;
    }
    private static string GetMiddle(string s) {
      return s.Substring(1, s.Length - 2);
    }
    private static bool IsValidMarkdownCode(string s) {
      return (s.Length >= 3) && IsFirstChar(s, '`') && IsLastChar(s, '`');
    }
    private static bool HasContent(string s, ref string trimmed) {
      if (string.IsNullOrEmpty(s)) {
        return false;
      }
      else {
        trimmed = s.Trim();
        return !string.IsNullOrEmpty(trimmed);
      }
    }
    public static string Embed(string s, string tag) {
      return String.Format("<{0}>{1}</{0}>", tag, s);
    }
    public string ConvertEntity(string s) {
      if (s == "?")
        return DumpEntityMap();
      if (EntityMap.ContainsKey(s))
    	  return EntityMap[s];
      return String.Format("&{0};", s);
    }
    public string DumpEntityMap() {
      string result = "Entity map:<br />";
      foreach (KeyValuePair<string, string> item in EntityMap) {
        result += String.Format("[{0}] => [{1}]<br />", item.Key, item.Value);
      }
      return result;
    }

    public void CreateEntityMap() {
      EntityMap = new Dictionary<string, string>();
      EntityMap.Add("sp","&nbsp;");         // non-breaking space
      EntityMap.Add(">>", "&#xbb;");        // chevron right
      EntityMap.Add("<<", "&#xab;");        // chevron left
      EntityMap.Add("&gt;&gt;", "&#xbb;");  // chevron right
      EntityMap.Add("&lt;&lt;", "&#xab;");  // chevron left
      EntityMap.Add("(c)", "&#xa9;");       // copyright
      EntityMap.Add("S", "&#xa7;");         // section mark
      EntityMap.Add("P", "&para;");         // paragraph mark
      EntityMap.Add("*", "&#x2022;");       // bullet
      EntityMap.Add(".", "&#xb7;");         // middle dot 
      EntityMap.Add("-", "&#x2013;");       // dash
      EntityMap.Add("--", "&#x2014;");      // long dash
      EntityMap.Add(":>", "&#x25b6;");      // large triangle pointing right
      EntityMap.Add(":&gt;", "&#x25b6;");   // large triangle pointing right
      EntityMap.Add("o", "&#x25e6;");       // degree symbol
      EntityMap.Add("[]", "&#x25ab;");      // small middle square
      EntityMap.Add("<>", "&#x25c7;");      // diamond
      EntityMap.Add("&lt;&gt;", "&#x25c7;");// diamond
      EntityMap.Add("1/2", "&frac12;");     // 1/2 as character
      EntityMap.Add("1/4", "&frac14;");     // 1/4 as character
      EntityMap.Add("3/4", "&frac34;");     // 3/4 as character
      EntityMap.Add("tm", "&trade;");       // trademark
      EntityMap.Add("(r)", "&reg;");        // registered trademark
      EntityMap.Add("...", "&hellip;");     // ellipsis
      EntityMap.Add("x", "&times;");        // multiply operator
      EntityMap.Add("/", "&divide;");       // divide operator
    }
  }
}
