from sqlalchemy import create_engine, Column, Integer, String, ForeignKey
from sqlalchemy.orm import relationship, declarative_base, sessionmaker

Base = declarative_base()


# Модель User
class User(Base):
    __tablename__ = "users"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    posts = relationship("Post", back_populates="user")


# Модель Post
class Post(Base):
    __tablename__ = "posts"
    id = Column(Integer, primary_key=True)
    title = Column(String, nullable=False)
    content = Column(String)
    user_id = Column(Integer, ForeignKey("users.id"))
    user = relationship("User", back_populates="posts")


# Подключение к базе и создание таблиц
engine = create_engine("postgresql+psycopg2://postgres:12345678@localhost/postgres")
Base.metadata.create_all(engine)

# CRUD-операции
Session = sessionmaker(bind=engine)
session = Session()

# Create
user = User(name="Alice")
post = Post(title="Hello, World!", content="My first post.", user=user)
session.add(user)
session.add(post)
session.commit()

# Read
user_from_db = session.query(User).filter_by(name="Alice").first()
print(user_from_db.name, user_from_db.posts[0].title)

# Update
user_from_db.name = "Alice Updated"
session.commit()

# Delete
session.delete(user_from_db)
session.commit()
