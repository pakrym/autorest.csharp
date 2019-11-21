using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;

namespace AutoRest.CSharp.V3
{
    public class CodeWriter
    {
        private readonly StringBuilder _codeBuilder = new StringBuilder();

        public CodeWriter Append(string? s)
        {
            _codeBuilder.Append(s);
            return this;
        }

        public CodeWriter AppendLine(string? s)
        {
            _codeBuilder.AppendLine(s);
            return this;
        }

        public CodeWriter AppendKeyword(string s)
        {
            return Append(s).Append(" ");
        }

        public CodeWriterScope Scope()
        {
            AppendLine("{");
            return new CodeWriterScope(this);
        }

        public class CodeWriterScope : IDisposable
        {
            private readonly CodeWriter _codeWriter;

            public CodeWriterScope(CodeWriter codeWriter)
            {
                _codeWriter = codeWriter;
            }

            public void Dispose()
            {
                _codeWriter.Append("}");
            }
        }

        public CodeWriter AppendName(string modelName)
        {
            return Append(modelName).Append(" ");
        }

        public CodeWriter Semicolon()
        {
            return Append(";");
        }

        public override string ToString()
        {
            return _codeBuilder.ToString();
        }

        public string ToFormattedCode()
        {
            var syntax = SyntaxFactory.ParseCompilationUnit(ToString());
            return Formatter.Format(syntax, new AdhocWorkspace()).ToFullString();
        }

        public CodeWriter AppendLiteral(object? value)
        {
            switch (value)
            {
                case null:
                    return Append("null");
                case string s:
                    return Append($"\"{s}\"");
                default:
                    return Append(value.ToString()!);
            }
        }

        public CodeWriter AppendVisibility(Visibility visibility)
        {
            return this.AppendKeyword(visibility.ToString().ToLowerInvariant());
        }

        public CodeWriter AppendModifiers(TypeModifiers modifiers)
        {
            if (modifiers == TypeModifiers.None)
            {
                return this;
            }
            return AppendKeyword(modifiers.ToString().ToLowerInvariant());
        }

        public CodeWriter AppendModifiers(MemberModifiers modifiers)
        {
            if (modifiers == MemberModifiers.None)
            {
                return this;
            }

            return AppendKeyword(modifiers.ToString().ToLowerInvariant());
        }
    }
}