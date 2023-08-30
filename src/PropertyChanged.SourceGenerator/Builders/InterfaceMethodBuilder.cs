using PropertyChanged.SourceGenerator.Abstractions.Syntax;
using PropertyChanged.SourceGenerator.Models.Analyzers;
using PropertyChanged.SourceGenerator.Models.Metadata;
using PropertyChanged.SourceGenerator.Models.Nodes;

namespace PropertyChanged.SourceGenerator.Builders;

/// <summary>
/// Interface raise method builder for <see cref="InterfaceAnalysis.EventSymbol"/> implementation member.
/// </summary>
public class InterfaceMethodBuilder : ISyntaxBuilder<MethodMetadata, InterfaceAnalysis>
{
    private readonly string methodName;
    private readonly InvocationMethodMetadata memberInvocation;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="methodName">Method name.</param>
    /// <param name="memberInvocation">Interface member invocation.</param>
    public InterfaceMethodBuilder(string methodName, InvocationMethodMetadata memberInvocation)
    {
        this.methodName = methodName;
        this.memberInvocation = memberInvocation;
    }

    /// <inheritdoc/>
    public MethodMetadata Build(InterfaceAnalysis analysis)
    {
        var method = new MethodMetadata()
        {
            Name = methodName,
            Modifier = MemberModifiers.Protected,
        };
        method.Invocations.Add(memberInvocation);

        var defaultParameter = new MethodParameterMetadata
        {
            Name = "propertyName",
            AttributeName = "System.Runtime.CompilerServices.CallerMemberNameAttribute",
            Type = typeof(string),
            IsNullable = true,
            Value = "null",
        };
        method.Parameters.Add(defaultParameter);

        return method;
    }
}
