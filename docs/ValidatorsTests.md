# Testes dos Validators

Este arquivo explica os testes presentes no projeto `Validators.Tests`, com foco no validator de cadastro de despesa:

```csharp
RegisterExpenseValidatorTests
```

Esses testes garantem que as regras de validação de uma despesa estão funcionando corretamente antes que a requisição siga para o restante da aplicação.

## O que está sendo testado?

O arquivo de teste valida o comportamento da classe:

```csharp
RegisterExpenseValidator
```

Essa classe define as regras para o objeto:

```csharp
RequestRegisterExpenseJson
```

Ou seja, os testes verificam se uma despesa enviada para cadastro possui dados válidos, como:

- título obrigatório;
- título com tamanho máximo;
- descrição com tamanho máximo;
- data obrigatória;
- data que não pode estar no futuro;
- valor maior que zero;
- tipo de pagamento válido.

## Teste de sucesso

O teste `Success` verifica o cenário em que a requisição está correta.

```csharp
[Fact]
public void Success()
{
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    
    ValidationResult? result = validator.Validate(instance: request);
    
    result.IsValid.Should().BeTrue();
}
```

Nesse teste:

- é criado um `RegisterExpenseValidator`;
- é criada uma requisição válida usando `RequestRegisterExpenseJsonBuilder`;
- a requisição é validada;
- o resultado precisa ser válido.

A intenção é garantir que, quando todos os campos estão corretos, o validator não bloqueia a requisição.

## Testes de erro

Os outros testes verificam cenários inválidos.

Por exemplo:

```csharp
[Fact]
public void Error_Title_Maximum_Length()
{
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Title = new string(c: 'a', count: 101);

    ValidationResult? result = validator.Validate(instance: request);

    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And
        .Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_TITLE_MAXIMUM_LENGTH));
}
```

Aqui o teste começa com uma requisição válida e altera apenas o campo que deseja testar.

Nesse caso, o título recebe `101` caracteres. Como a regra permite no máximo `100`, o resultado esperado é inválido.

Além disso, o teste também confere se a mensagem de erro retornada é exatamente:

```csharp
ResourceErrorMessage.EXPENSE_TITLE_MAXIMUM_LENGTH
```

Isso é importante porque não basta a validação falhar. Ela precisa falhar pelo motivo certo.

## Padrão AAA

Os testes seguem o padrão AAA:

- Arrange;
- Act;
- Assert.

Esse padrão ajuda a organizar o teste em três etapas.

## Arrange

É a preparação do teste.

Aqui criamos os objetos necessários para executar o cenário:

```csharp
RegisterExpenseValidator validator = new();
RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
request.Title = "";
```

No projeto, normalmente o `Arrange` cria:

- o validator;
- uma request válida;
- uma alteração específica para deixar o cenário inválido.

## Act

É a execução da ação que está sendo testada.

No caso dos validators, a ação principal é chamar o método `Validate`:

```csharp
ValidationResult? result = validator.Validate(instance: request);
```

## Assert

É a verificação do resultado.

Exemplo:

```csharp
result.IsValid.Should().BeFalse();
result.Errors.Should().ContainSingle().And
    .Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED));
```

Aqui o teste confirma que:

- a validação falhou;
- existe apenas um erro;
- o erro retornado é o erro esperado.

## Por que usar um Builder nos testes?

O projeto usa a classe:

```csharp
RequestRegisterExpenseJsonBuilder
```

Ela cria uma requisição válida para os testes:

```csharp
public static RequestRegisterExpenseJson Build()
{
    Faker faker = new();
    
    return new RequestRegisterExpenseJson
    {
        Title = faker.Commerce.ProductName(),
        Description = faker.Commerce.ProductDescription(),
        Date =  faker.Date.Past(),
        Amount = faker.Random.Decimal(min: 1, max: 10000),
        PaymentType = faker.PickRandom<PaymentType>(),
    };
}
```

Isso evita repetição dentro dos testes.

Em vez de preencher todos os campos manualmente em cada teste, o builder entrega uma request válida. Depois, cada teste altera somente o campo que deseja validar.

Exemplo:

```csharp
RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
request.Amount = 0;
```

Assim o teste fica mais claro, porque o foco está apenas na regra testada.

## Bogus

O `Bogus` é uma biblioteca usada para gerar dados falsos para testes.

No projeto, ele aparece no builder:

```csharp
Faker faker = new();
```

Com o `Faker`, conseguimos gerar dados como:

```csharp
Title = faker.Commerce.ProductName();
Description = faker.Commerce.ProductDescription();
Date = faker.Date.Past();
Amount = faker.Random.Decimal(min: 1, max: 10000);
PaymentType = faker.PickRandom<PaymentType>();
```

Isso ajuda porque os testes não dependem de valores fixos escritos manualmente.

O `Bogus` é útil para:

- reduzir código repetido;
- gerar objetos completos rapidamente;
- deixar os testes mais próximos de dados reais;
- facilitar a criação de cenários válidos.

Mesmo usando dados aleatórios, o builder precisa respeitar as regras do validator. Por exemplo:

- a data gerada deve estar no passado;
- o valor deve ser maior que zero;
- o tipo de pagamento deve existir no enum.

Assim, quando um teste falhar, a chance é maior de o problema estar na regra específica alterada pelo teste.

## FluentAssertions

O `FluentAssertions` é uma biblioteca que deixa as validações dos testes mais legíveis.

Em vez de escrever asserts mais tradicionais, podemos escrever frases próximas da linguagem natural:

```csharp
result.IsValid.Should().BeTrue();
```

Ou:

```csharp
result.IsValid.Should().BeFalse();
```

Também é possível validar coleções:

```csharp
result.Errors.Should().ContainSingle();
```

E combinar validações:

```csharp
result.Errors.Should().ContainSingle().And
    .Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED));
```

Esse estilo deixa claro o que o teste espera:

- que o resultado seja válido;
- que o resultado seja inválido;
- que exista apenas um erro;
- que a mensagem de erro seja a esperada.

## Fact

O `[Fact]` é um atributo do xUnit usado para marcar um teste simples.

Ele é usado quando o teste não precisa receber parâmetros externos.

Exemplo:

```csharp
[Fact]
public void Error_Date_In_The_Future()
{
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Date = DateTime.Today.AddDays(value: 1);

    ValidationResult? result = validator.Validate(instance: request);

    result.IsValid.Should().BeFalse();
}
```

Esse teste sempre executa o mesmo cenário: uma data no futuro.

Use `[Fact]` quando existe apenas um caso específico a ser testado.

## Theory

O `[Theory]` é um atributo do xUnit usado quando o mesmo teste deve rodar várias vezes com dados diferentes.

No projeto, ele aparece junto com `[InlineData]`.

Exemplo:

```csharp
[Theory]
[InlineData(data: "")]
[InlineData(data: " ")]
[InlineData(data: null)]
public void Error_Title_Empty(string title)
{
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Title = title;
    
    ValidationResult? result = validator.Validate(instance: request);

    result.IsValid.Should().BeFalse();
}
```

Esse teste será executado três vezes:

- uma vez com `""`;
- uma vez com `" "`;
- uma vez com `null`.

Todos esses valores representam um título inválido.

Outro exemplo:

```csharp
[Theory]
[InlineData(data: 0)]
[InlineData(data: -1)]
public void Error_Amount_Invalid(decimal amount)
{
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Amount = amount;

    ValidationResult? result = validator.Validate(instance: request);

    result.IsValid.Should().BeFalse();
}
```

Esse teste valida que o valor da despesa não pode ser `0` nem negativo.

Use `[Theory]` quando a mesma regra precisa ser testada com diferentes entradas.

## Diferença entre Fact e Theory

| Atributo | Quando usar | Exemplo |
| --- | --- | --- |
| `[Fact]` | Quando o teste tem um cenário fixo | data no futuro |
| `[Theory]` | Quando o teste roda com diferentes valores | título vazio, espaço ou nulo |

## Estrutura dos testes no projeto

Os testes seguem uma estratégia simples:

1. Criar uma requisição válida com o builder.
2. Alterar apenas o campo que será testado.
3. Executar o validator.
4. Verificar se o resultado é válido ou inválido.
5. Quando inválido, verificar se a mensagem de erro é a correta.

Essa abordagem deixa os testes mais confiáveis, porque cada teste valida uma regra por vez.

## Exemplo completo seguindo AAA

```csharp
[Fact]
public void Error_Payment_Type_Invalid()
{
    // Arrange
    RegisterExpenseValidator validator = new();
    RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.PaymentType = (PaymentType)100;

    // Act
    ValidationResult? result = validator.Validate(instance: request);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And
        .Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_PAYMENT_TYPE_INVALID));
}
```

Esse teste força um valor inválido para o enum `PaymentType`.

Como `(PaymentType)100` não representa um tipo de pagamento válido, o validator deve retornar erro.

## Resumo

Os testes do `Validators.Tests` garantem que as regras do `RegisterExpenseValidator` estão corretas.

O projeto usa:

- xUnit para estruturar e executar os testes;
- `[Fact]` para cenários fixos;
- `[Theory]` e `[InlineData]` para cenários com várias entradas;
- Bogus para gerar dados falsos válidos;
- FluentAssertions para escrever verificações mais legíveis;
- padrão AAA para organizar cada teste em preparação, execução e validação.

Com isso, os testes ficam mais claros, menos repetitivos e mais fáceis de manter.
