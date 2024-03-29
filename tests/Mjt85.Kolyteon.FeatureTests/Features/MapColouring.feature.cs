﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Mjt85.Kolyteon.FeatureTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class MapColouringFeature : object, Xunit.IClassFixture<MapColouringFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "MapColouring.feature"
#line hidden
        
        public MapColouringFeature(MapColouringFeature.FixtureData fixtureData, Mjt85_Kolyteon_FeatureTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Map Colouring", "As a developer, I want to represent any Map Colouring puzzle in code so that I ca" +
                    "n model and solve it as a binary CSP.", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Create a serializable puzzle")]
        [Xunit.TraitAttribute("FeatureTitle", "Map Colouring")]
        [Xunit.TraitAttribute("Description", "Create a serializable puzzle")]
        public void CreateASerializablePuzzle()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a serializable puzzle", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table1.AddRow(new string[] {
                            "PresetMap",
                            "Rwanda"});
                table1.AddRow(new string[] {
                            "GlobalColours",
                            "Black,Cyan,Magenta,Yellow"});
#line 5
        testRunner.Given("I have created a Map Colouring puzzle as follows", ((string)(null)), table1, "Given ");
#line hidden
#line 9
        testRunner.And("I have serialized the Map Colouring puzzle to JSON", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
        testRunner.When("I deserialize a Map Colouring puzzle from the JSON", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
        testRunner.Then("the deserialized Map Colouring puzzle should be the same as the original puzzle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Confirm a proposed solution is valid")]
        [Xunit.TraitAttribute("FeatureTitle", "Map Colouring")]
        [Xunit.TraitAttribute("Description", "Confirm a proposed solution is valid")]
        public void ConfirmAProposedSolutionIsValid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Confirm a proposed solution is valid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table2.AddRow(new string[] {
                            "PresetMap",
                            "Australia"});
                table2.AddRow(new string[] {
                            "GlobalColours",
                            "Red,Blue,Green"});
#line 14
        testRunner.Given("I have created a Map Colouring puzzle as follows", ((string)(null)), table2, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Region",
                            "Colour"});
                table3.AddRow(new string[] {
                            "WA",
                            "Red"});
                table3.AddRow(new string[] {
                            "NT",
                            "Green"});
                table3.AddRow(new string[] {
                            "SA",
                            "Blue"});
                table3.AddRow(new string[] {
                            "Q",
                            "Red"});
                table3.AddRow(new string[] {
                            "NSW",
                            "Green"});
                table3.AddRow(new string[] {
                            "V",
                            "Red"});
                table3.AddRow(new string[] {
                            "T",
                            "Green"});
#line 18
        testRunner.And("I have obtained the following region/colour dictionary as a proposed solution to " +
                        "the Map Colouring puzzle", ((string)(null)), table3, "And ");
#line hidden
#line 27
        testRunner.When("I ask the Map Colouring puzzle to validate the proposed solution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 28
        testRunner.Then("the validation result should be successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Model a puzzle as a binary CSP")]
        [Xunit.TraitAttribute("FeatureTitle", "Map Colouring")]
        [Xunit.TraitAttribute("Description", "Model a puzzle as a binary CSP")]
        public void ModelAPuzzleAsABinaryCSP()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Model a puzzle as a binary CSP", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 30
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table4.AddRow(new string[] {
                            "PresetMap",
                            "Australia"});
                table4.AddRow(new string[] {
                            "GlobalColours",
                            "Red,Blue,Green"});
#line 31
        testRunner.Given("I have created a Map Colouring puzzle as follows", ((string)(null)), table4, "Given ");
#line hidden
#line 35
        testRunner.And("I have modelled the Map Colouring puzzle as a binary CSP", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 36
        testRunner.When("I request the binary CSP metrics for the Map Colouring puzzle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table5.AddRow(new string[] {
                            "Variables",
                            "7"});
                table5.AddRow(new string[] {
                            "Constraints",
                            "9"});
                table5.AddRow(new string[] {
                            "ConstraintDensity",
                            "0.428571"});
                table5.AddRow(new string[] {
                            "ConstraintTightness",
                            "0.333333"});
#line 37
        testRunner.Then("the binary CSP problem metrics should be as follows", ((string)(null)), table5, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table6.AddRow(new string[] {
                            "MinimumValue",
                            "3"});
                table6.AddRow(new string[] {
                            "MeanValue",
                            "3"});
                table6.AddRow(new string[] {
                            "MaximumValue",
                            "3"});
                table6.AddRow(new string[] {
                            "DistinctValues",
                            "1"});
#line 43
        testRunner.And("the binary CSP variable domain size statistics should be as follows", ((string)(null)), table6, "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table7.AddRow(new string[] {
                            "MinimumValue",
                            "0"});
                table7.AddRow(new string[] {
                            "MeanValue",
                            "2.571429"});
                table7.AddRow(new string[] {
                            "MaximumValue",
                            "5"});
                table7.AddRow(new string[] {
                            "DistinctValues",
                            "4"});
#line 49
        testRunner.And("the binary CSP variable degree statistics should be as follows", ((string)(null)), table7, "And ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table8.AddRow(new string[] {
                            "MinimumValue",
                            "0"});
                table8.AddRow(new string[] {
                            "MeanValue",
                            "0.857143"});
                table8.AddRow(new string[] {
                            "MaximumValue",
                            "1.666667"});
                table8.AddRow(new string[] {
                            "DistinctValues",
                            "4"});
#line 55
        testRunner.And("the binary CSP variable sum tightness statistics should be as follows", ((string)(null)), table8, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Solve a binary CSP modelling a solvable puzzle")]
        [Xunit.TraitAttribute("FeatureTitle", "Map Colouring")]
        [Xunit.TraitAttribute("Description", "Solve a binary CSP modelling a solvable puzzle")]
        [Xunit.InlineDataAttribute("Backtracking", "None", new string[0])]
        [Xunit.InlineDataAttribute("Backtracking", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("Backtracking", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("Backtracking", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("Backjumping", "None", new string[0])]
        [Xunit.InlineDataAttribute("Backjumping", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("Backjumping", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("Backjumping", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("GraphBasedBackjumping", "None", new string[0])]
        [Xunit.InlineDataAttribute("GraphBasedBackjumping", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("GraphBasedBackjumping", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("GraphBasedBackjumping", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("ConflictDirectedBackjumping", "None", new string[0])]
        [Xunit.InlineDataAttribute("ConflictDirectedBackjumping", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("ConflictDirectedBackjumping", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("ConflictDirectedBackjumping", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("ForwardChecking", "None", new string[0])]
        [Xunit.InlineDataAttribute("ForwardChecking", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("ForwardChecking", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("ForwardChecking", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("PartialLookingAhead", "None", new string[0])]
        [Xunit.InlineDataAttribute("PartialLookingAhead", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("PartialLookingAhead", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("PartialLookingAhead", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("FullLookingAhead", "None", new string[0])]
        [Xunit.InlineDataAttribute("FullLookingAhead", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("FullLookingAhead", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("FullLookingAhead", "MaxTightness", new string[0])]
        [Xunit.InlineDataAttribute("MaintainingArcConsistency", "None", new string[0])]
        [Xunit.InlineDataAttribute("MaintainingArcConsistency", "Brelaz", new string[0])]
        [Xunit.InlineDataAttribute("MaintainingArcConsistency", "MaxCardinality", new string[0])]
        [Xunit.InlineDataAttribute("MaintainingArcConsistency", "MaxTightness", new string[0])]
        public void SolveABinaryCSPModellingASolvablePuzzle(string search, string ordering, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Search", search);
            argumentsOfScenario.Add("Ordering", ordering);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Solve a binary CSP modelling a solvable puzzle", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 62
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table9.AddRow(new string[] {
                            "PresetMap",
                            "Australia"});
                table9.AddRow(new string[] {
                            "GlobalColours",
                            "Red,Blue,Green"});
#line 63
        testRunner.Given("I have created a Map Colouring puzzle as follows", ((string)(null)), table9, "Given ");
#line hidden
#line 67
        testRunner.And("I have modelled the Map Colouring puzzle as a binary CSP", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 68
        testRunner.And(string.Format("I have set the Map Colouring binary CSP solver to use the \'{0}\' search strategy", search), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
        testRunner.And(string.Format("I have set the Map Colouring binary CSP solver to use the \'{0}\' ordering strategy" +
                            "", ordering), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 70
        testRunner.When("I run the Map Colouring binary CSP solver on the binary CSP", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 71
        testRunner.And("I ask the Map Colouring puzzle to validate the proposed solution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 72
        testRunner.Then("the validation result should be successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                MapColouringFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                MapColouringFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
