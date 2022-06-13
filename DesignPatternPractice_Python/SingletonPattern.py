from functools import wraps


class SingletonPattern:
    # %% Use function decorator
    class ByFunctionDecoratorMethod:
        @staticmethod
        def run():
            def singleton_wrapper(cls):
                __instance = dict()  # closure for access

                @wraps(cls)
                def wrapper():
                    if cls not in __instance:
                        __instance[cls] = cls()  # Note: cls is identical and hashable

                    return __instance[cls]

                return wrapper

            @singleton_wrapper
            class MyClass:
                pass

            instance_1 = MyClass()
            instance_2 = MyClass()

            print(f"id(instance_1)={id(instance_1)}, id(instance_2)={id(instance_2)}")
            assert id(instance_1) == id(instance_2)
            print("id equal")

    # %% Use class decorator
    class ByClassDecoratorMethod:
        @staticmethod
        def run():
            class DecoratorClass:
                def __init__(self, cls):
                    self.cls = cls
                    self.__instance = dict()  # member variable for access

                def __call__(self):
                    if self.cls not in self.__instance:
                        self.__instance[self.cls] = self.cls()

                    return self.__instance[self.cls]

            @DecoratorClass
            class MyClass:
                pass

            instance_1 = MyClass()
            instance_2 = MyClass()

            print(f"id(instance_1)={id(instance_1)}, id(instance_2)={id(instance_2)}")
            assert id(instance_1) == id(instance_2)
            print("id equal")

    # %% Update class __new__
    class ByNew:
        @staticmethod
        def run():
            class MyClass:
                __instance = dict()  # class member variable for access

                def __new__(cls, *args, **kwargs):
                    if cls not in cls.__instance:
                        cls.__instance[cls] = super().__new__(cls, *args, **kwargs)

                    return cls.__instance[cls]

            instance_1 = MyClass()
            instance_2 = MyClass()

            print(f"id(instance_1)={id(instance_1)}, id(instance_2)={id(instance_2)}")
            assert id(instance_1) == id(instance_2)
            print("id equal")

    # %% By meta programming
    class ByMetaProgramming1:
        @staticmethod
        def run():
            class MetaClass(type):
                # class member of MetaClass
                __instance = dict()

                def __call__(cls, *args, **kwargs):
                    if cls not in cls.__instance:
                        cls.__instance[cls] = super().__call__(*args, **kwargs)

                    return cls.__instance[cls]

            class MyClass(metaclass=MetaClass):
                pass

            instance_1 = MyClass()
            instance_2 = MyClass()

            print(f"id(instance_1)={id(instance_1)}, id(instance_2)={id(instance_2)}")
            assert id(instance_1) == id(instance_2)
            print("id equal")

    # %% By meta programming
    class ByMetaProgramming2:
        @staticmethod
        def run():
            class MetaClass(type):
                def __new__(mcs, *args, **kwargs):
                    # class object is still an object
                    mcs.__instance = dict()  # class member of MyClass class (which is an object of MetaClass)
                    return super().__new__(mcs, *args, **kwargs)

                def __call__(cls, *args, **kwargs):
                    if cls not in cls.__instance:
                        cls.__instance[cls] = super().__call__(*args, **kwargs)

                    return cls.__instance[cls]

            class MyClass(metaclass=MetaClass):
                pass

            instance_1 = MyClass()
            instance_2 = MyClass()

            print(f"id(instance_1)={id(instance_1)}, id(instance_2)={id(instance_2)}")
            assert id(instance_1) == id(instance_2)
            print("id equal")
