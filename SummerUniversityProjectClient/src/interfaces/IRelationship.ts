import { IBaseEntity } from "./IBaseEntity";
import { IPersonData } from "./IPersonData";

export interface IRelationship extends IBaseEntity{
    relation: string,
    personInRelationshipId: number,
    personData: IPersonData
}