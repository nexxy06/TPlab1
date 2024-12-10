from sqlalchemy import create_engine, Column, Integer, String, ForeignKey
from sqlalchemy.orm import relationship, declarative_base, sessionmaker

Base = declarative_base()


class Category (Base):
    __tablename__ = "category"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    # показывает двухстороннюю связь но не является атрибутом таблицы
    products = relationship("Product", back_populates="category")


class Product(Base):
    __tablename__ = "product"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    price = Column(Integer)
    category_id = Column(Integer, ForeignKey("category.id"))
    # тоже не атрибут, а связь таблиц, в в sql его не было видно
    category = relationship("Category", back_populates="products")


# Подключение к базе и создание таблиц
engine = create_engine("postgresql+psycopg2://postgres:12345678@localhost/postgres")
Base.metadata.create_all(engine)

# CRUD-операции
Session = sessionmaker(bind=engine)
session = Session()

# Create
bikes = Category(name="bike")
prod = Product(name="black bike", price="200000", category=bikes)
session.add(bikes)
session.add(prod)
session.commit()

# Read
bikes_from_db = session.query(Category).filter_by(name="bike").first()
print("Категория: {} \n Товар: {} \n Цена: {}".format(bikes_from_db.name, bikes_from_db.products[0].name, bikes_from_db.products[0].price))

# Update
bikes_from_db.name = "bike new"
session.commit()

# Delete
session.delete(bikes_from_db)
session.commit()
