
const fs = require('fs');
const data = JSON.parse(fs.readFileSync('c:/Users/daniela.resende/Downloads/Prototipo1/Vibe Code/Projetos_Separados/Integracao_Suzano/js/vtc_queue.json'));
const debugStatus = new Set(data.map(p => p.status));
console.log('Unique statuses:', Array.from(debugStatus));

