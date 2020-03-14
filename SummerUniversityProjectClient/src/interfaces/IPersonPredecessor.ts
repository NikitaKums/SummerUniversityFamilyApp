import { IPersonData } from "./IPersonData";

export interface IPersonPredecessor {
    personData: IPersonData,
    predecessorCount: number,
    predecessors: [IPersonData]
}