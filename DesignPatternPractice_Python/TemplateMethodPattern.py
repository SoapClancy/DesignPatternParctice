from abc import ABCMeta, abstractmethod
from typing import Type
from functools import wraps


class TemplateMethodPattern:
    @staticmethod
    def run():
        class Counter:
            def __init__(self):
                self.i = 1

        def counter_decorator(counter: Counter):
            def inner_wrapper(func):
                @wraps(func)
                def decorated_func(*args, **kwargs):
                    print(f"Step {counter.i}: ")
                    counter.i += 1
                    return func(*args, **kwargs)

                return decorated_func

            return inner_wrapper

        class AbstractDish(metaclass=ABCMeta):
            @abstractmethod
            def prepare(self):
                pass

            @abstractmethod
            def cook(self):
                pass

            @abstractmethod
            def apply_cumin(self):
                pass

            @abstractmethod
            def eat(self):
                pass

            @abstractmethod
            def clear(self):
                pass

            # Note: this is Hook Method, i.e., the method that have impacts on the template method
            def allow_apply_cumin(self):  # default, apply cumin
                return True

            def happy_time(self):
                self.prepare()
                self.cook()
                if self.allow_apply_cumin():
                    self.eat()
                self.eat()
                self.clear()

        class GrillBeef(AbstractDish):
            counter = Counter()

            def __init__(self):
                self.allow_apply_cumin_flag = False

            def set_allow_apply_cumin_flag(self, flag: bool):  # Customizable
                self.allow_apply_cumin_flag = flag

            @counter_decorator(counter)
            def prepare(self):
                print("Prepare grill beef")

            @counter_decorator(counter)
            def cook(self):
                print("Cook grill beef")

            @counter_decorator(counter)
            def apply_cumin(self):
                print("Add some cumin to beef")

            @counter_decorator(counter)
            def eat(self):
                print("Eat grill beef")

            @counter_decorator(counter)
            def clear(self):
                print("Clear grill beef\n")

            def allow_apply_cumin(self):
                return self.allow_apply_cumin_flag

        class GrillLamb(AbstractDish):
            counter = Counter()

            @counter_decorator(counter)
            def prepare(self):
                print("Prepare grill lamb")

            @counter_decorator(counter)
            def cook(self):
                print("Cook grill lamb")

            @counter_decorator(counter)
            def apply_cumin(self):
                print("Add some cumin to lamb")

            @counter_decorator(counter)
            def eat(self):
                print("Eat grill lamb")

            @counter_decorator(counter)
            def clear(self):
                print("Clear grill lamb\n")

            def allow_apply_cumin(self):  # Always apply cumin for lamb
                return True

        grill_beef = GrillBeef()
        grill_beef.happy_time()
        grill_lamb = GrillLamb()
        grill_lamb.happy_time()
