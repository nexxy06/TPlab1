import unittest


class bankAccount:
    def __init__(self, accNumber: str, balance: float = 0):
        if balance < 0:
            raise ValueError("initial balance cannot be negative")
        self.accNumber = accNumber
        self.balance = balance

    def deposit(self, amount: float):
        if amount <= 0:
            raise ValueError("deposit amount must be greater than zero")
        self.balance += amount

    def withdraw(self, amount: float):
        if amount <= 0:
            raise ValueError("withdrawal amount must be greater than zero")
        if amount > self.balance:
            raise ValueError("Insufficient funds")
        self.balance -= amount

    def getBalance(self):
        return self.balance


try:
    acc = bankAccount("123", 100)
    print(f"initial balance: {acc.getBalance()}")
    acc.deposit(50)
    print(f"balance after deposit: {acc.getBalance()}")
    acc.withdraw(30)
    print(f"balance after withdrawal: {acc.getBalance()}")

    acc.withdraw(150)
except ValueError as e:
    print(e)


class testBankAccount(unittest.TestCase):

    def test_positive_balance(self):
        acc = bankAccount("123", 100)
        self.assertEqual(acc.getBalance(), 100)

    def test_negative_balance(self):
        with self.assertRaises(ValueError):
            bankAccount("123", -100)

    def test_deposit(self):
        acc = bankAccount("123", 100)
        acc.deposit(50.0)
        self.assertEqual(acc.getBalance(), 150)

    def test_deposit_invalid_amount(self):
        acc = bankAccount("123", 100)
        with self.assertRaises(ValueError):
            acc.deposit(0)
        with self.assertRaises(ValueError):
            acc.deposit(-50.0)

    def test_withdraw_success(self):
        acc = bankAccount("123", 100)
        acc.withdraw(50.0)
        self.assertEqual(acc.getBalance(), 50)

    def test_withdraw_insufficient_funds(self):
        acc = bankAccount("123", 100)
        with self.assertRaises(ValueError):
            acc.withdraw(150.0)

    def test_withdraw_invalid_amount(self):
        acc = bankAccount("123", 100)
        with self.assertRaises(ValueError):
            acc.withdraw(0)
        with self.assertRaises(ValueError):
            acc.withdraw(-50.0)

    def test_balance_after_operations(self):
        acc = bankAccount("123", 100)
        acc.deposit(50.0)
        acc.withdraw(30.0)
        self.assertEqual(acc.getBalance(), 120)


if __name__ == "__main__":
    unittest.main()
