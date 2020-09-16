﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Botframework.LUParser.parser
{
    public class NestedIntentSection: Section
    {
        public List<SimpleIntentSection> SimpleIntentSections { get; set; }


        public NestedIntentSection(LUFileParser.NestedIntentSectionContext parseTree, string content)
        {
            this.SectionType = SectionType.NestedIntentSection;
            this.Name = ExtractName(parseTree);
            this.Body = String.Empty;

        }

        public string ExtractName(LUFileParser.NestedIntentSectionContext parseTree)
        {
            return parseTree.nestedIntentNameLine().nestedIntentName().GetText().Trim();
        }

        public List<SimpleIntentSection> ExtractSimpleIntentSections(LUFileParser.NestedIntentSectionContext parseTree, string content)
        {
            var simpleIntentSections = new List<SimpleIntentSection>();
            foreach (var subIntentDefinition in parseTree.nestedIntentBodyDefinition().subIntentDefinition())
            {
                var simpleIntentSection = new SimpleIntentSection(subIntentDefinition.simpleIntentSection(), content);
                simpleIntentSection.Range.Start.Character = 0;
                simpleIntentSections.Add(simpleIntentSection);
            }

            return simpleIntentSections;
        }
    }
}