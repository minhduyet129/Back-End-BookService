import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import About from "./components/About";
import Home from "./components/Home";



function App() {
  return (
    <Router>
      <nav className='navbar navbar-inverse'>
        <div className='navbar-brand'>Book Derectory</div>
        <ul className='nav navbar-nav'>
          <li className='active'>
            <Link exact to="/">Home</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
        </ul>
      </nav>

      <Route path="/" exact><Home/></Route>
      <Route path="/about"><About/></Route>
    </Router>
  );
}

export default App;
