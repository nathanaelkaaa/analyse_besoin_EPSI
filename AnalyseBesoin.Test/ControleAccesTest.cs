using AnalyseBesoin.Test.Utilities;

namespace AnalyseBesoin.Test;

public class ControleAccesTest
{
    [Fact]
    public void CasNominal()
    {
        // ETANT DONNE une Porte reli�e � un Lecteur, ayant d�tect� un Badge
        var porte = new PorteSpy();
        var lecteur = new LecteurFake();

        lecteur.SimulerD�tectionBadge();

        var moteurOuverture = new MoteurOuverture();
        moteurOuverture.Associer(lecteur, porte);

        // QUAND le Moteur d'Ouverture effectue une interrogation des lecteurs
        moteurOuverture.Interroger();

        // ALORS le signal d'ouverture est envoy� � la porte
        Assert.True(porte.OuvertureDemand�e);
    }

    [Fact]
    public void CasAucuneInterrogation()
    {
        // ETANT DONNE une Porte reli�e � un Lecteur, ayant d�tect� un Badge
        var porte = new PorteSpy();
        var lecteur = new LecteurFake();

        lecteur.SimulerD�tectionBadge();

        var moteurOuverture = new MoteurOuverture();
        moteurOuverture.Associer(lecteur, porte);

        // ALORS le signal d'ouverture n'est pas envoy� � la porte
        Assert.False(porte.OuvertureDemand�e);
    }

    [Fact]
    public void CasNonBadg�()
    {
        // ETANT DONNE une Porte reli�e � un Lecteur, n'ayant pas d�tect� un Badge
        var porte = new PorteSpy();
        var lecteur = new LecteurFake();

        var moteurOuverture = new MoteurOuverture();
        moteurOuverture.Associer(lecteur, porte);

        // QUAND le Moteur d'Ouverture effectue une interrogation des lecteurs
        moteurOuverture.Interroger();

        // ALORS le signal d'ouverture n'est pas envoy� � la porte
        Assert.False(porte.OuvertureDemand�e);
    }

    [Fact]
    public void DeuxPortes()
    {
        // ETANT DONNE un Lecteur ayant d�tect� un Badge
        // ET un autre Lecteur n'ayant rien d�tect�
        // ET une Porte reli�e chacune � un Lecteur
        var porteDevantSOuvrir = new PorteSpy();
        var porteDevantResterFerm�e = new PorteSpy();

        var lecteurAyantD�tect� = new LecteurFake();
        lecteurAyantD�tect�.SimulerD�tectionBadge();

        var lecteurNAyantPasD�tect� = new LecteurFake();

        var moteurOuverture = new MoteurOuverture();
        moteurOuverture.Associer(lecteurAyantD�tect�, porteDevantSOuvrir);
        moteurOuverture.Associer(lecteurNAyantPasD�tect�, porteDevantResterFerm�e);

        // QUAND le Moteur d'Ouverture effectue une interrogation des lecteurs
        moteurOuverture.Interroger();

        // ALORS seule la Porte reli�e au Lecteur re�oit le signal d'ouverture
        Assert.False(porteDevantResterFerm�e.OuvertureDemand�e);
        Assert.True(porteDevantSOuvrir.OuvertureDemand�e);
    }

    [Fact]
    public void DeuxPortesMaisLinverse()
    {
        // ETANT DONNE un Lecteur ayant d�tect� un Badge
        // ET un autre Lecteur n'ayant rien d�tect�
        // ET une Porte reli�e chacune � un Lecteur
        var porteDevantSOuvrir = new PorteSpy();
        var porteDevantResterFerm�e = new PorteSpy();

        var lecteurAyantD�tect� = new LecteurFake();
        lecteurAyantD�tect�.SimulerD�tectionBadge();

        var lecteurNAyantPasD�tect� = new LecteurFake();

        var moteurOuverture = new MoteurOuverture();
        moteurOuverture.Associer(lecteurNAyantPasD�tect�, porteDevantResterFerm�e);
        moteurOuverture.Associer(lecteurAyantD�tect�, porteDevantSOuvrir);

        // QUAND le Moteur d'Ouverture effectue une interrogation des lecteurs
        moteurOuverture.Interroger();

        // ALORS seule la Porte reli�e au Lecteur re�oit le signal d'ouverture
        Assert.False(porteDevantResterFerm�e.OuvertureDemand�e);
        Assert.True(porteDevantSOuvrir.OuvertureDemand�e);
    }
}