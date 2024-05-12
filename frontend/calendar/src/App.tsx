import Calendar from './components/Calendar.tsx';
import '../styles/app.scss';
import { useEffect, useState } from 'react';

function App() {
  const [chosenHours, setChosenHours] = useState([]);
  useEffect(() => {
    const fetchChosenHours = async () => {
      const res = await fetch('https://localhost:7162/chosenhour');
      const data = await res.json();
      setChosenHours(data);
    };
    fetchChosenHours();
  }, []);

  return (
    <div className='app'>
      <Calendar events={chosenHours} />
    </div>
  );
}

export default App;
