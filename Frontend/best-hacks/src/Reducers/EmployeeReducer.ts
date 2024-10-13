import {createSlice} from '@reduxjs/toolkit';
import {Employee} from "../Models/Interfaces";
import {employeesMock} from "../Swipe/EmployeesMock";

interface employeesState {
    index: number
    employees: Employee[],
    currentEmployee: Employee
}

const initialState: employeesState = {
    index: 0,
    employees: employeesMock,
    currentEmployee: employeesMock[0]
}

const employeeSlice = createSlice({
    name: 'employee',
    initialState,
    reducers: {
        updateEmployees: (state, action) => {
            return {index: 0, employees: action.payload, currentEmployee: action.payload[0]}
        },
        getNext: (state) => {
            if (state.employees[state.index + 1] == undefined) {
                return {...state, index: 0}
                //api call to get next
            }
            return {...state, index: state.index + 1}
        }
    },
    selectors: {
        selectEmployee: (state) => state.employees[state.index]
    }
});

export const {updateEmployees, getNext} = employeeSlice.actions;
export const {selectEmployee} = employeeSlice.selectors;
export default employeeSlice.reducer;
