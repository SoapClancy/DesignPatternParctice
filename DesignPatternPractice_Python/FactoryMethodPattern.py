from abc import ABCMeta, abstractmethod
from typing import Type


class FactoryMethodPattern:
    @staticmethod
    def run():
        class Animal(metaclass=ABCMeta):
            @abstractmethod
            def speak(self):
                pass

        class Dog(Animal):
            def __init__(self, age: int = None):
                self.age = age

            def speak(self):
                print("I am a dog")

            @staticmethod
            def play_ball():
                print("Dog plays ball")

            def say_age(self):
                print(f"Dog with age = {self.age}")

        class Cat(Animal):
            def speak(self):
                print("I am a cat")

            @staticmethod
            def catch_mouse():
                print("Cat catches mouse")

        class AbstractAnimalFactory(metaclass=ABCMeta):
            @abstractmethod
            def create(self, cls: Type[Animal], *args, **kwargs) -> Animal:
                pass

        class AnimalFactory(AbstractAnimalFactory):
            def create(self, cls: Type[Animal], *args, **kwargs) -> Animal:
                try:
                    obj = cls(*args, **kwargs)
                except:
                    raise f"Fail to create {type(cls)}"

                return obj

        animal_factory = AnimalFactory()
        dog = animal_factory.create(Dog)  # type: Dog
        dog.speak()
        print(type(dog))
        dog.play_ball()

        cat = animal_factory.create(Cat)  # type: Cat
        cat.speak()
        print(type(cat))

        dog_with_age = animal_factory.create(Dog, 10)  # type: Dog
        dog_with_age.say_age()
