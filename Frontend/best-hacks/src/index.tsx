import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Login from './Auth/Login';
import { Provider } from 'react-redux';
import { store } from './store'
import 'bootstrap/dist/css/bootstrap.css';
import Register from './Auth/Register';
//import Chat from './Chat/Chat';
import SwipeScreen from './Swipe/SwipeScreen'
import 'bootstrap/dist/css/bootstrap.css';
import TopBar from './Utils/TopBar';
import Matches from './Chat/Matches';


const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/login",
    element: <Login />
  },
  {
    path: "/swipe",
    element: <SwipeScreen/>
  },
  {
    path: "/register",
    element: <Register />
  },
  // {
  //   path: "/chat/:matchId", // Nowa trasa dla czatu
  //   element: <Chat />, // Usuń matchId z props
  // },
  {
    path: "/chat",
    element: <Matches />
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <TopBar />
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
