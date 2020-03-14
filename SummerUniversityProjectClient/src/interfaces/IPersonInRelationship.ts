import { IBaseEntity } from "./IBaseEntity";
import { IRelationship } from "./IRelationship";
import { IPersonData } from "./IPersonData";

export interface IPersonInRelationship extends IBaseEntity{
    personData: IPersonData,
    personRelatedTo: [IRelationship],
    personRelatedFrom: [IRelationship]
}