import { IBaseEntity } from "./IBaseEntity";
import { IPersonInRelationship } from "./IPersonInRelationship";
import { IPersonData } from "./IPersonData";

export interface IPerson extends IPersonData{
    relatedFrom: [IPersonInRelationship],
    relatedTo: [IPersonInRelationship],
    relationCount: number
}