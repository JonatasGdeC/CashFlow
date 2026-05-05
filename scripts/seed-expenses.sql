-- MySQL seed for CashFlowDb.
-- PaymentType: 0 = Cash, 1 = CreditCard, 2 = DebitCard, 3 = Electronic.
-- If your client is not already connected to the target database, uncomment:
-- USE CashFlowDb;

INSERT INTO Expenses
    (Id, Title, Description, Date, Amount, PaymentType)
VALUES
    (UUID(), 'Supermercado', 'Compras do mes', '2026-05-01 10:15:00.000000', 356.78, 2),
    (UUID(), 'Aluguel', 'Pagamento do aluguel mensal', '2026-05-02 09:00:00.000000', 1800.00, 3),
    (UUID(), 'Energia eletrica', 'Conta de luz', '2026-04-28 14:30:00.000000', 219.45, 3),
    (UUID(), 'Agua', 'Conta de agua', '2026-04-27 13:20:00.000000', 87.90, 3),
    (UUID(), 'Internet', 'Plano de internet residencial', '2026-04-25 08:45:00.000000', 129.99, 1),
    (UUID(), 'Combustivel', 'Abastecimento do carro', '2026-05-03 18:10:00.000000', 250.00, 2),
    (UUID(), 'Farmacia', 'Medicamentos e itens pessoais', '2026-05-04 11:05:00.000000', 96.35, 1),
    (UUID(), 'Academia', 'Mensalidade da academia', '2026-04-10 07:30:00.000000', 119.90, 1),
    (UUID(), 'Restaurante', 'Almoco fora', '2026-05-04 12:40:00.000000', 68.50, 0),
    (UUID(), 'Transporte por app', 'Corrida para compromisso', '2026-05-02 19:25:00.000000', 32.80, 1),
    (UUID(), 'Streaming', 'Assinatura mensal', '2026-04-15 06:00:00.000000', 39.90, 1),
    (UUID(), 'Curso online', 'Assinatura de plataforma de estudos', '2026-04-18 20:15:00.000000', 79.90, 3),
    (UUID(), 'Padaria', 'Cafe da manha', '2026-05-01 07:50:00.000000', 24.60, 0),
    (UUID(), 'Manutencao do carro', 'Troca de oleo e filtro', '2026-04-22 16:00:00.000000', 310.00, 2),
    (UUID(), 'Roupas', 'Compra de camiseta e calca', '2026-04-20 15:35:00.000000', 189.90, 1),
    (UUID(), 'Mercado pet', 'Racao e itens de higiene', '2026-04-19 17:45:00.000000', 142.75, 2),
    (UUID(), 'Consulta medica', 'Consulta particular', '2026-04-12 10:00:00.000000', 250.00, 3),
    (UUID(), 'Estacionamento', 'Estacionamento no centro', '2026-05-03 14:05:00.000000', 18.00, 0),
    (UUID(), 'Presente', 'Presente de aniversario', '2026-04-30 18:40:00.000000', 120.00, 1),
    (UUID(), 'Material de escritorio', 'Caderno, canetas e organizador', '2026-04-26 09:10:00.000000', 73.25, 2);
