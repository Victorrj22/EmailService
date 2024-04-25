using EmailService.Models;

namespace EmailService.Repository;

public class Repository
{
    /// <summary>
    /// Busca todos os endereços de emails a serem notificados
    /// </summary>
    /// <returns></returns>
    public List<string> FindEmails()
    {
        using (var dbContext = new PostgresContext())
        {
            return dbContext.Configuracaoemails.Select(e => e.Ceemail).ToList();
        }
    }

    /// <summary>
    /// Busca todos os produtos que precisam ser avisados
    /// </summary>
    /// <returns></returns>
    public List<Produto> FindProductToNotice()
    {
        using (var dbContext = new PostgresContext())
        {
            return dbContext.Produtos.Where(p => p.Proqtdestoque < p.Proqtdavisa).ToList();
        }

    }

    /// <summary>
    /// Verifica a data do ultimo aviso de um determinado produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    public Aviso? FindNoticeDate(Produto produto)
    {
        using (var dbContext = new PostgresContext())
        {
            return dbContext.Avisos.Where(a => a.Avcodigoproduto == produto.Procodigo)
                .OrderByDescending(a => a.Avdata).FirstOrDefault();
        }
    }

    /// <summary>
    /// Verifica se existem avisos
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    public bool CheckNotices(Produto produto)
    {
        using (var dbContext = new PostgresContext())
        {
            return dbContext.Avisos.Any(a => a.Avcodigoproduto == produto.Procodigo);
        }
    }

    /// <summary>
    /// Adiciona um novo registro de aviso
    /// </summary>
    /// <param name="produto"></param>
    public void AddNotice(Produto produto)
    {
        using (var dbContext = new PostgresContext())
        {
            var aviso = new Aviso()
            {
                Avdata = DateTime.Now,
                Avcodigoproduto = produto.Procodigo
            };
            dbContext.Avisos.Add(aviso);
            dbContext.SaveChanges();
        }
    }
}