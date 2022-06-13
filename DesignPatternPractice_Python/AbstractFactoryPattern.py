from abc import ABCMeta, abstractmethod
from typing import Type


class AbstractFactoryPattern:
    @staticmethod
    def run():
        class Animal(metaclass=ABCMeta):
            @abstractmethod
            def speak(self):
                pass

        class Dog(Animal):
            pass

        class DogXY(Dog):
            def speak(self):
                print("I am a XY dog")

        class DogXX(Dog):
            def speak(self):
                print("I am a XX dog")

        class AnimalFactory(metaclass=ABCMeta):
            @abstractmethod
            def create_dog(self) -> Animal:
                pass

        class AnimalXYFactory(AnimalFactory):
            def create_dog(self) -> Animal:
                return DogXY()

        class AnimalXXFactory(AnimalFactory):
            def create_dog(self) -> Animal:
                return DogXX()

        dog_xy_factory = AnimalXYFactory()  # type: AnimalXYFactory
        dog_xy_factory.create_dog().speak()

        dog_xx_factory = AnimalXXFactory()  # type: AnimalXYFactory
        dog_xx_factory.create_dog().speak()
