import { IBaseEntity } from "./IBaseEntity";
import { IPersonData } from "./IPersonData";
import { IRelationship } from "./IRelationship";

export interface IPersonInRelationshipEdit extends IBaseEntity {
    person: IPersonData,
    person1: IPersonData,
    relationship: IRelationship,
    relationshipId: number
}