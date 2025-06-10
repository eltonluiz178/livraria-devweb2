export interface Emprestimo {
  id : number
  dataEmprestimo : Date
  dataDevolucaoPrevista : Date
  dataDevolucaoReal : Date
  multa : number
  status : string
  clienteId : number
  livroId : number
}
