import {combineReducers} from '@reduxjs/toolkit'
import authReducer from './Reducers/AuthReducer'
import employeeReducer from "./Reducers/EmployeeReducer";

export default combineReducers({
    authReducer,
    employeeReducer
})
