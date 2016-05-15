using System;

namespace RubiusTestTaskModule
{
    // интерфейс, обьединяющий  классов графических компонент
    internal interface IChangeable
    {
        // метод просматривает связанные с обьектом вью-компоненты, извлекает из них данные, и обновляет объект
        void ApplyChange();
    }

}