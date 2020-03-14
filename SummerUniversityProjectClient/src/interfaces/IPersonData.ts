import { IBaseEntity } from "./IBaseEntity";

export interface IPersonData extends IBaseEntity {
    firstName: string,
    lastName: string,
    age: number
}