using System.Threading.Tasks;
using MagicxorAnalyzer.CSharp.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = MagicxorAnalyzer.CSharp.Test.Verifiers.CSharpAnalyzerVerifier<
    MagicxorAnalyzer.CSharp.MagicxorAnalyzerCSharpAnalyzer>;

namespace MagicxorAnalyzer.CSharp.Test;

[TestClass]
public class PrimaryConstructorsNotAllowedRuleTests
{
    [TestMethod]
    public async Task NotTriggered_When_NoCodePresent()
    {
        const string test = "";
        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Class_HasNoConstructorAndProps()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    class SomeClass
                                    {
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Class_HasNoConstructor()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public class SomeClass
                                    {
                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                        public virtual int SomeProp2 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Class_HasNormalConstructorWithoutArg()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public class SomeClass
                                    {
                                        public SomeClass() {}

                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                        public virtual int SomeProp2 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Class_HasNormalConstructorWithArg()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public class SomeClass
                                    {
                                        public SomeClass(string str) {}

                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                        public virtual int SomeProp2 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Struct_HasNoConstructorAndProps()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    struct SomeStruct
                                    {
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Struct_HasNoConstructor()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public struct SomeStruct
                                    {
                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Struct_HasNormalConstructorWithArg()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public struct SomeStruct
                                    {
                                        public SomeStruct(string str) : this() {}

                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Record_HasNormalConstructorWithoutArg()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public record SomeRecord
                                    {
                                        public SomeRecord() {}

                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Record_HasNormalConstructorWithArg()
    {
        const string test = """
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;
                                using System.Diagnostics;

                                namespace ConsoleApplication1
                                {
                                    public record SomeRecord
                                    {
                                        public SomeRecord(string str) {}

                                        public DateTime SomeDateTime { get; set; }
                                        public int SomeProp1 { get; set; }
                                    }
                                }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Record_HasPrimaryConstructorWithBody()
    {
        const string test = """
                            namespace ConsoleApp1;

                            record Record(int A = 10)
                            {
                                public int A { get; } = A;
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_Record_HasPrimaryConstructorWithoutBody()
    {
        const string test = """
                            namespace System.Runtime.CompilerServices
                            {
                                  internal static class IsExternalInit {}
                            }

                            namespace ConsoleApp1
                            {
                                record Record(int A = 10);
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_RecordClass_HasPrimaryConstructorWithBody()
    {
        const string test = """
                            namespace ConsoleApp1;

                            record class RecordClass(int A = 10)
                            {
                                public int A { get; } = A;
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_RecordClass_HasPrimaryConstructorWithoutBody()
    {
        const string test = """
                            namespace System.Runtime.CompilerServices
                            {
                                  internal static class IsExternalInit {}
                            }

                            namespace ConsoleApp1
                            {
                                record class RecordClass(int A = 10);
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_RecordStruct_HasPrimaryConstructorWithBody()
    {
        const string test = """
                            namespace ConsoleApp1;

                            record struct RecordStruct(int A)
                            {
                                public int A { get; } = A;
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task NotTriggered_When_RecordStruct_HasPrimaryConstructorWithoutBody()
    {
        const string test = """
                            namespace ConsoleApp1;

                            record struct RecordStruct(int A);
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task Triggered_When_Class_HasPrimaryConstructor()
    {
        const string test = """
                            namespace ConsoleApp1;

                            class Class(int A = 10)
                            {
                                public int A { get; } = A;
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test,
            VerifyCS.Diagnostic(Rules.PrimaryConstructorsNotAllowedRule.Descriptor)
                .WithSpan(3, 7, 3, 12)
                .WithArguments(".ctor"));
    }

    [TestMethod]
    public async Task Triggered_When_Struct_HasPrimaryConstructor()
    {
        const string test = """
                            namespace ConsoleApp1;

                            struct Struct(int A)
                            {
                                public int A { get; } = A;
                            }
                            """;

        await VerifyCS.VerifyAnalyzerAsync(test,
            VerifyCS.Diagnostic(Rules.PrimaryConstructorsNotAllowedRule.Descriptor)
                .WithSpan(3, 8, 3, 14)
                .WithArguments(".ctor"));
    }
}
