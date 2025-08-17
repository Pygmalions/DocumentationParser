using System.Reflection;
using System.Text;
using DocumentationParser.Utilities;

namespace DocumentationParser;

/// <summary>
/// This static helper class can format the entry name for specified reflection information.
/// </summary>
public static class EntryName
{
    private static readonly ObjectPool<StringBuilder> StringBuilders = 
        new(() => new StringBuilder());

    public static string Of(Type type)
    {
        return $"T:{type.FullName}".Replace('+', '.');
    }

    public static string Of(ConstructorInfo constructor)
        => BuildName('M', constructor, "#ctor").Replace('+', '.');

    public static string Of(FieldInfo field)
        => BuildName('F', field).Replace('+', '.');

    public static string Of(PropertyInfo property)
        => BuildName('P', property).Replace('+', '.');

    public static string Of(EventInfo @event)
        => BuildName('E', @event).Replace('+', '.');

    public static string Of(MethodInfo method)
    {
        var builder = StringBuilders.Rent();

        builder.Append("M:");
        if (method.DeclaringType != null)
        {
            builder.Append(method.DeclaringType.FullName?.Replace('+', '.'));
            builder.Append('.');
        }

        builder.Append(method.Name);
        builder.Append('(');
        builder.AppendJoin(',', method.GetParameters().Select(
            parameter => parameter.ParameterType.FullName?.Replace('+', '.')));
        builder.Append(')');

        var name = builder.ToString();

        builder.Clear();
        StringBuilders.Return(builder);
        return name;
    }

    private static string BuildName(char symbol, MemberInfo member, string? memberName = null)
    {
        memberName ??= member.Name;
        
        var builder = StringBuilders.Rent();
        builder.Append(symbol);
        builder.Append(':');
        if (member.DeclaringType != null)
        {
            builder.Append(member.DeclaringType.FullName?.Replace('+', '.'));
            builder.Append('.');
        }

        builder.Append(memberName);
        var name = builder.ToString();
        
        builder.Clear();
        StringBuilders.Return(builder);

        return name;
    }

    public static string Of(MemberInfo member)
    {
        return member switch
        {
            MethodInfo method => Of(method),
            FieldInfo field => Of(field),
            PropertyInfo property => Of(property),
            EventInfo @event => Of(@event),
            ConstructorInfo constructor => Of(constructor),
            _ => throw new Exception(
                $"Unsupported member type {member.MemberType} for member {member.DeclaringType}.{member.Name}")
        };
    }
}