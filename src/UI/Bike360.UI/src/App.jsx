import { 
  Route, 
  createBrowserRouter, 
  createRoutesFromElements, 
  RouterProvider 
} from 'react-router-dom';
import HomePage from './pages/HomePage';
import MainLayout from './layouts/MainLayout';
import BikesPage from './pages/BikesPage';

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path='/' element={ <MainLayout /> }>
      <Route index element={ <HomePage /> }/>
      <Route path='/bikes' element={ <BikesPage /> }/>
    </Route>
  )
);

const App = () =>{
  return  <RouterProvider router={ router }/>;
};

export default App;