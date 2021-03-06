' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.CodeAnalysis.Completion.Providers
Imports Microsoft.CodeAnalysis.VisualBasic.Completion.Providers

Namespace Microsoft.CodeAnalysis.Editor.VisualBasic.UnitTests.Completion.CompletionProviders
    Public Class PartialTypeCompletionProviderTests
        Inherits AbstractVisualBasicCompletionProviderTests

        Friend Overrides Function CreateCompletionProvider() As ICompletionProvider
            Return New PartialTypeCompletionProvider()
        End Function

        <WorkItem(578224)>
        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub RecommendTypesWithoutPartial()
            Dim text = <text>Class C
End Class

Partial Class $$</text>

            VerifyItemExists(text.Value, "C")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialClass1()
            Dim text = <text>Partial Class C
End Class

Partial Class $$</text>

            VerifyItemExists(text.Value, "C")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialGenericClass1()
            Dim text = <text>Class Bar
End Class
                           
Partial Class C(Of Bar)
End Class

Partial Class $$</text>

            VerifyItemExists(text.Value, "C(Of Bar)")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialGenericClassCommitOnParen()
            Dim text = <text>Class Bar
End Class
                           
Partial Class C(Of Bar)
End Class

Partial Class $$</text>

            Dim expected = <text>Class Bar
End Class
                           
Partial Class C(Of Bar)
End Class

Partial Class C(Of Bar)</text>

            VerifyProviderCommit(text.Value, "C(Of Bar)", expected.Value, "("c, "", Microsoft.CodeAnalysis.SourceCodeKind.Regular)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialGenericClassCommitOnTab()
            Dim text = <text>Class Bar
End Class
                           
Partial Class C(Of Bar)
End Class

Partial Class $$</text>

            Dim expected = <text>Class Bar
End Class
                           
Partial Class C(Of Bar)
End Class

Partial Class C(Of Bar)</text>

            VerifyProviderCommit(text.Value, "C(Of Bar)", expected.Value, Nothing, "", Microsoft.CodeAnalysis.SourceCodeKind.Regular)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialClassWithModifiers()
            Dim text = <text>Partial Class C
End Class

Partial Protected Class $$</text>

            VerifyItemExists(text.Value, "C")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialStruct()
            Dim text = <text>Partial Structure S
End Structure

Partial Structure $$</text>

            VerifyItemExists(text.Value, "S")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialInterface()
            Dim text = <text>Partial Interface I
End Interface

Partial Interface $$</text>

            VerifyItemExists(text.Value, "I")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialModule()
            Dim text = <text>Partial Module M
End Module

Partial Module $$</text>

            VerifyItemExists(text.Value, "M")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub TypeKindMatches1()
            Dim text = <text>Partial Structure S
End Structure

Partial Class $$</text>

            VerifyNoItemsExist(text.Value)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub TypeKindMatches2()
            Dim text = <text>Partial Class C
End Class

Partial Structure $$</text>

            VerifyNoItemsExist(text.Value)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub PartialClassesInSameNamespace()
            Dim text = <text>Namespace N
    Partial Class Foo

    End Class
End Namespace

Namespace N
    Partial Class $$

End Namespace</text>

            VerifyItemExists(text.Value, "Foo")
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub NotPartialClassesAcrossDifferentNamespaces()
            Dim text = <text>Namespace N
    Partial Class Foo

    End Class
End Namespace

Partial Class $$</text>

            VerifyNoItemsExist(text.Value)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub IncludeConstraints()
            Dim text = <text>
Partial Class C1(Of T As Exception)
 
End Class

Partial Class $$</text>

            VerifyItemExists(text.Value, "C1(Of T As Exception)")
        End Sub

        <WorkItem(578122)>
        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub DoNotSuggestCurrentMember()
            Dim text = <text>
Partial Class F$$
                       </text>

            VerifyNoItemsExist(text.Value)
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Completion)>
        Public Sub NotInTrivia()
            Dim text = <text>
Partial Class C1
 
End Class

Partial Class '$$</text>

            VerifyNoItemsExist(text.Value)
        End Sub

    End Class
End Namespace

