
const fs = require('fs');
const data = JSON.parse(fs.readFileSync('debug_vtc.json'));

let todosVtc = data;

let filaAtiva = todosVtc.filter(p => {
    const norm = (str) => str ? str.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().trim() : '';
    const obs = norm(p.observacoes);
    const st = norm(p.status);

    const statusPermitido = st === 'em analise' || st === 'em andamento' || st === 'nao concluido' || st === 'aguardando analise';

    return statusPermitido &&
        !obs.includes('finalizad') &&
        !obs.includes('concluid') &&
        !obs.includes('devolvid') &&
        !obs.includes('exigencia');
});

filaAtiva.sort((a, b) => {
    const dateA = a.data_entrada ? new Date(a.data_entrada) : new Date(8640000000000000); 
    const dateB = b.data_entrada ? new Date(b.data_entrada) : new Date(8640000000000000);
    return dateA - dateB;
});

const nilza = filaAtiva.find(p => p.nome && p.nome.includes('NILZA'));
if (nilza) {
    console.log('NILZA FOUND IN ACTIVE QUEUE!');
    const index = filaAtiva.findIndex(p => p.nome === nilza.nome);
    console.log('INDEX:', index);
    console.log('QUEUE LENGTH:', filaAtiva.length);
    filaAtiva.forEach((p, i) => console.log(i, p.nome, p.data_entrada));
} else {
    console.log('NILZA NOT ACTIVE!');
}

