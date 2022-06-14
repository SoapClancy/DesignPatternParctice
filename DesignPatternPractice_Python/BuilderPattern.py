from abc import ABCMeta, abstractmethod
from typing import List


class BuilderPattern:
    @staticmethod
    def run():
        class AbstractDish(metaclass=ABCMeta):
            def __init__(self):
                self.__procedures = []  # type: List[str]

            # This is a template method
            def set_procedures(self, procedures: List[str]):
                self.__procedures = procedures

            @abstractmethod
            def _add_spicy(self):
                pass

            @abstractmethod
            def _add_salt(self):
                pass

            @abstractmethod
            def _add_cumin(self):
                pass

            # This is a template method
            def happy_time(self):
                for procedure in self.__procedures:
                    if procedure == "add_spicy":
                        self._add_spicy()
                    elif procedure == "add_salt":
                        self._add_salt()
                    elif procedure == "add_cumin":
                        self._add_cumin()
                    else:
                        raise ValueError()

        class GrillBeef(AbstractDish):
            def _add_spicy(self):
                print("Add spicy to beef")

            def _add_salt(self):
                print("Add salt to beef")

            def _add_cumin(self):
                print("Add cumin to beef")

        class GrillLamb(AbstractDish):
            def _add_spicy(self):
                print("Add spicy to lamb")

            def _add_salt(self):
                print("Add salt to lamb")

            def _add_cumin(self):
                print("Add cumin to lamb")

        class Builder(metaclass=ABCMeta):
            @abstractmethod
            def get_dish(self) -> AbstractDish:
                pass

            # This is a template method
            def set_procedures(self, procedures: List[str]):
                self.get_dish().set_procedures(procedures)

        class GrillBeefBuilder(Builder):
            def __init__(self):
                self.grill_beef = GrillBeef()  # type: GrillBeef

            def get_dish(self) -> AbstractDish:
                return self.grill_beef

        class GrillLambBuilder(Builder):
            def __init__(self):
                self.grill_lamb = GrillBeef()  # type: GrillLamb

            def get_dish(self) -> AbstractDish:
                return self.grill_lamb

        class Director:
            @staticmethod
            def get_spicy_salt_grill_beef(grill_beef_builder: Builder) -> AbstractDish:
                grill_beef_builder.set_procedures(["add_spicy", "add_salt"])

                return grill_beef_builder.get_dish()

            @staticmethod
            def get_spicy_salt_grill_cumin_beef(grill_beef_builder: Builder) -> AbstractDish:
                grill_beef_builder.set_procedures(["add_spicy", "add_salt", "add_cumin"])

                return grill_beef_builder.get_dish()

            @staticmethod
            def get_cumin_lamb(grill_lamb_builder: Builder) -> AbstractDish:
                grill_lamb_builder.set_procedures(["add_cumin"])

                return grill_lamb_builder.get_dish()

        # Section 1
        spicy_salt_grill_beef = Director.get_spicy_salt_grill_beef(GrillBeefBuilder())
        spicy_salt_grill_beef.happy_time()
        print("=" * 79)

        # Section 2
        spicy_salt_grill_cumin_beef = Director.get_spicy_salt_grill_cumin_beef(GrillBeefBuilder())
        spicy_salt_grill_cumin_beef.happy_time()
        print("=" * 79)

        # Section 3
        spicy_salt_grill_beef.happy_time()
        print("=" * 79)

        # Section 4
        cumin_lamb = Director.get_cumin_lamb(GrillLambBuilder())
        cumin_lamb.happy_time()
        print("=" * 79)

        # Section 5
        spicy_salt_grill_beef.happy_time()
        print("=" * 79)
