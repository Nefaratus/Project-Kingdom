using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

	public class SettingList
	{
		public SettingList()
		{
			indexFields();
		}
		public SettingList(String file)
		{
			fillFields(file);
		}
		public override string ToString()
		{
			String s = "";
			listFields(ref s);
			return s;
		}
		private Dictionary<string, Object> fields;
		// identifier, value
		public void indexFields()
		{
			fields = new Dictionary<string, Object>();
			foreach (System.Reflection.FieldInfo field in this.GetType().GetFields())
			{
				fields.Add(field.Name, field.GetValue(this) == null ? "" : field.GetValue(this));
			}
		}
		public void listFields(ref String listedFields, int depth = 0)
		{
			String preString = "";
			for (int i = 0; i < depth; i++)
			{
				preString += " ";
			}
			indexFields();
			foreach (KeyValuePair<string, Object> kvp in fields)
			{
				if (kvp.Value.GetType().IsSubclassOf(typeof(SettingList)))
				{
					listedFields += (preString + "<" + kvp.Key + ":" + kvp.Value.GetType().ToString() + ">\n");
					SettingList cc = (SettingList)kvp.Value;
					cc.listFields(ref listedFields, depth + 1);
					listedFields += (preString + "</" + kvp.Key + ">\n");
				}
				else
				{
					listedFields += (preString + "<" + kvp.Key + ":" + kvp.Value.GetType().ToString() + ">" + Convert.ToString(kvp.Value) + "</" + kvp.Key + ">\n");
				}
			}
		}
		public void fillFields(String file)
		{
			indexFields();
			Regex getTagsFull = new Regex(@"<((.)+?):.+?>(.|\n)*?</\1>", RegexOptions.None);
			MatchCollection hits = getTagsFull.Matches(file);
			foreach (Match m in hits)
			{
				String[] s = m.ToString().Split(new Char[] { '<', ':', '>' });
				for (int i = 0; i < s.Length; i++)
				{
					foreach (System.Reflection.FieldInfo field in this.GetType().GetFields())
					{
						
						if (field.Name == s[1] && field.FieldType.ToString() == s[2])
						{
							if (field.FieldType.IsSubclassOf(typeof(SettingList)))
							{
								SettingList cc = (SettingList)(field.GetValue(this));
								cc.fillFields(m.ToString());
							}
							else
							{
								field.SetValue(this, Convert.ChangeType(s[3], Type.GetType(s[2])));
							}
						}
					}
				}
			}
		}
	}