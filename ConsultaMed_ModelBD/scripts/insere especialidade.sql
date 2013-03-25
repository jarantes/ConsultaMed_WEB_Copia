insert into dbo.Especialidade(Descricao) values ('Odontologia')
insert into dbo.Especialidade(Descricao) values ('Ortopedia')
insert into dbo.Especialidade(Descricao) values ('Otorrinolaringologia')
insert into dbo.Especialidade(Descricao) values ('Dermatologia')
insert into dbo.Especialidade(Descricao) values ('Ginicologia')
insert into dbo.Especialidade(Descricao) values ('Cardiologia')
insert into dbo.Especialidade(Descricao) values ('Neurologia')
insert into dbo.Especialidade(Descricao) values ('Alergologia')
insert into dbo.Especialidade(Descricao) values ('Anestesiologia')
insert into dbo.Especialidade(Descricao) values ('Angiologia')
insert into dbo.Especialidade(Descricao) values ('Endocrinologia')
insert into dbo.Especialidade(Descricao) values ('Endoscopia')
insert into dbo.Especialidade(Descricao) values ('Fonoaudiologia')
insert into dbo.Especialidade(Descricao) values ('Gastroenterologia')
insert into dbo.Especialidade(Descricao) values ('Infectologia')
insert into dbo.Especialidade(Descricao) values ('Mastologia')
insert into dbo.Especialidade(Descricao) values ('Oftalmologia')
insert into dbo.Especialidade(Descricao) values ('Pediatria')
insert into dbo.Especialidade(Descricao) values ('Pneumologia')
insert into dbo.Especialidade(Descricao) values ('Proctologia')
insert into dbo.Especialidade(Descricao) values ('Psiquiatria')
insert into dbo.Especialidade(Descricao) values ('Reumatologia')
insert into dbo.Especialidade(Descricao) values ('Ultrassonografia')
insert into dbo.Especialidade(Descricao) values ('Urologia')
insert into dbo.Especialidade(Descricao) values ('Nefrologia')
insert into dbo.Especialidade(Descricao) values ('Tomografia')

select * from dbo.Paciente pac, dbo.Usuario us, dbo.Endereco en
where pac.UserId = us.UserId
and	  us.EnderecoId = en.EnderecoId

select * from dbo.Especialidade
where Descricao like '%Neur%'
union
select * from dbo.Especialidade
where Descricao like '%Derm%'
order by Descricao

drop table dbo.Especialidade